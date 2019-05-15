namespace portfoliosimulation.backend.adapters.alphavantage
{
    public class ExchangeRateToEuro
    {
        public string FromCurrency { get; }
        public double Rate { get; }

        public ExchangeRateToEuro(string fromCurrency, double rate)
        {
            FromCurrency = fromCurrency;
            Rate = rate;
        }
    }
}