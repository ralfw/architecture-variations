namespace portfoliosimulation.backend.adapters.alphavantage
{
    public class StockInfo
    {
        public string Symbol { get; }
        public string Name { get; }
        public string Region { get; }
        public string Currency { get; }

        public StockInfo(string symbol, string name, string region, string currency) {
            Symbol = symbol;
            Name = name;
            Region = region;
            Currency = currency;
        }
    }
}