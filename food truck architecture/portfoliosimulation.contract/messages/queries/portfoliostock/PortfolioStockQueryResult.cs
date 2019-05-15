using messagehandling;

namespace portfoliosimulation.contract.messages.queries.portfoliostock
{
    public class PortfolioStockQueryResult : QueryResult
    {
        public (string Name, string Symbol)[] MatchingStocks;
    }
}