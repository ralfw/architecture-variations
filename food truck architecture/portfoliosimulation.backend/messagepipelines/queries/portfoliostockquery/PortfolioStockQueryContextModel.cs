using messagehandling.pipeline.messagecontext;

namespace portfoliosimulation.backend.messagepipelines.queries.portfoliostockquery
{
    public class PortfolioStockQueryContextModel : IMessageContext
    {
        public (string Name, string Symbol)[] MatchingStocks;
    }
}