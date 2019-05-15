using System.Linq;
using portfoliosimulation.backend.domain;
using portfoliosimulation.contract;
using portfoliosimulation.contract.data.domain;
using portfoliosimulation.contract.messages.queries.portfolio;

namespace portfoliosimulation.backend.messagehandlers
{
    public class PortfolioQueryHandler : IPortfolioQueryHandling {
        private readonly IPortfolioRepository _repo;

        public PortfolioQueryHandler(IPortfolioRepository repo) { _repo = repo; }
        
        
        public PortfolioQueryResult Handle(PortfolioQuery request)
        {
            var portfolio = _repo.Load();
            var portfolioReturns = PortfolioManager.CalculateReturns(portfolio);
            return Result();

            
            PortfolioQueryResult Result() {
                return new PortfolioQueryResult {
                    PortfolioValue = portfolio.Entries.Sum(e => e.Qty * e.CurrentPrice),
                    PortfolioRateOfReturn = portfolioReturns.TotalRateOfReturn,
                    Stocks = portfolio.Entries.Select(Map).ToArray()
                };


                PortfolioQueryResult.StockInfo Map(Portfolio.Stock e)
                    => new PortfolioQueryResult.StockInfo {
                        Name = e.Name,
                        Symbol = e.Symbol,
                        Currency = e.Currency,
                        Qty = e.Qty,
                        BuyingPrice = e.BuyingPrice,
                        CurrentPrice = e.CurrentPrice,
                        
                        BuyingValue = e.Qty * e.BuyingPrice,
                        CurrentValue = e.Qty * e.CurrentPrice,
                        
                        Return = portfolioReturns.Returns[e.Symbol].Return,
                        RateOfReturn = portfolioReturns.Returns[e.Symbol].RateOfReturn
                    };
            }
        }
    }
}