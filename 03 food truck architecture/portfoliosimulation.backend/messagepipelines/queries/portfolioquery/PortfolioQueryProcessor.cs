using System.Linq;
using messagehandling;
using messagehandling.pipeline;
using messagehandling.pipeline.messagecontext;
using portfoliosimulation.backend.domain;
using portfoliosimulation.contract.data;
using portfoliosimulation.contract.messages.queries.portfolio;

namespace portfoliosimulation.backend.messagepipelines.queries.portfolioquery
{
    public class PortfolioQueryProcessor : IMessageProcessor {
        public Output Process(IMessage _, IMessageContext model) {
            var queryModel = model as PortfolioQueryContextModel;

            var prices = new Portfolio
            {
                Entries = queryModel.Entries
                    .Select(e => new Portfolio.Stock
                    {
                        Qty = e.Qty,
                        Bought = e.Bought,
                        BuyingPrice = e.BuyingPrice,
                        Currency = e.Currency,
                        CurrentPrice = e.CurrentPrice,
                        LastUpdated = e.LastUpdated,
                        Name = e.Name,
                        Symbol = e.Symbol
                    })
                    .ToList()
            };
            var returns = PortfolioManager.CalculateReturns(prices);
            return new QueryOutput(Result());
            
            
            PortfolioQueryResult Result() {
                return new PortfolioQueryResult {
                    PortfolioValue = queryModel.Entries.Sum(e => e.Qty * e.CurrentPrice),
                    PortfolioRateOfReturn = returns.TotalRateOfReturn,
                    Stocks = queryModel.Entries.Select(Map).ToArray()
                };


                PortfolioQueryResult.StockInfo Map(PortfolioQueryContextModel.StockInfo e)
                    => new PortfolioQueryResult.StockInfo {
                        Name = e.Name,
                        Symbol = e.Symbol,
                        Currency = e.Currency,
                        Qty = e.Qty,
                        BuyingPrice = e.BuyingPrice,
                        CurrentPrice = e.CurrentPrice,
                        
                        BuyingValue = e.Qty * e.BuyingPrice,
                        CurrentValue = e.Qty * e.CurrentPrice,
                        
                        Return = returns.Returns[e.Symbol].Return,
                        RateOfReturn = returns.Returns[e.Symbol].RateOfReturn
                    };
            }
        }
    }
}