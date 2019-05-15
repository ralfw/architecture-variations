using messagehandling;

namespace portfoliosimulation.contract.messages.queries.portfolio
{
    public class PortfolioQueryResult : QueryResult
    {
        public class StockInfo
        {
            public string Name;
            public string Symbol;
            public string Currency;
            public int Qty;
            public decimal BuyingPrice;
            public decimal BuyingValue;
            public decimal CurrentPrice;
            public decimal CurrentValue;
            public decimal Return;
            public decimal RateOfReturn;
        }

        public StockInfo[] Stocks;

        public decimal PortfolioValue;
        public decimal PortfolioRateOfReturn;
    }
}