using System.Collections.Generic;

namespace portfoliosimulation.contract
{
    public class CandidateStockInfo  {
        public string Name;
        public string Symbol;
        public string Currency;
        public decimal Price;
    }
    
    
    public interface IStockExchangeProvider
    {
        (string Symbol, decimal Price)[] GetPrice(params string[] symbols);

        CandidateStockInfo[] FindCandidates(string pattern);
    }
}