namespace portfoliosimulation.backend.adapters.alphavantage
{
    public interface IAlphaVantageProvider
    {
        StockPrice GetQuote(string symbol);
        ExchangeRateToEuro GetConversionRateToEuro(string fromCurrencyAbbreviation);
        StockInfo[] FindMatchingStocks(string pattern);
    }
}