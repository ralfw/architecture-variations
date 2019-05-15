using messagehandling;
using messagehandling.pipeline;
using messagehandling.pipeline.messagecontext;
using portfoliosimulation.contract.messages.queries.portfoliostock;

namespace portfoliosimulation.backend.messagepipelines.queries.portfoliostockquery
{
    public class PortfolioStockQueryProcessor : IMessageProcessor
    {
        public Output Process(IMessage _, IMessageContext model) {
            var queryModel = model as PortfolioStockQueryContextModel;
            
            return new QueryOutput(new PortfolioStockQueryResult {
                MatchingStocks = queryModel.MatchingStocks
            });
        }
    }
}