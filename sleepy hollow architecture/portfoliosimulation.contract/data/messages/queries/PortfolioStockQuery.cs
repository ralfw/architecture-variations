namespace portfoliosimulation.contract.data.messages.queries
{
    public class PortfolioStockQuery
    {
        public string Pattern;
    }

    public class PortfolioStockQueryResult
    {
        public (string Name, string Symbol)[] MatchingStocks;
    }
}