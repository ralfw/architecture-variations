using System;
using System.Collections.Generic;
using System.Linq;

namespace portfoliosimulation.contract.data
{
    public class Portfolio
    {
        public class Stock
        {
            public string Name;
            public string Symbol;
            public string Currency;
            public DateTime Bought;
            public int Qty;
            public decimal BuyingPrice;
            public decimal CurrentPrice;
            public DateTime LastUpdated;
        }

        public List<Stock> Entries = new List<Stock>();


        public string[] StockSymbols => Entries.Select(e => e.Symbol).Distinct().ToArray();


        public Stock[] Find(string pattern)
            => Entries.Where(e => e.Name.IndexOf(pattern, StringComparison.CurrentCultureIgnoreCase) >= 0 || 
                                  e.Symbol.IndexOf(pattern, StringComparison.CurrentCultureIgnoreCase) >= 0).ToArray();
        
        
        public void Remove(string symbol) {
            Entries = Entries.Where(e => e.Symbol != symbol).ToList();
        }
        
        
        public void Update(params (string Symbol, decimal Price)[] prices) 
            => prices.ToList().ForEach(p => Update(p.Symbol, p.Price));
        
        public void Update(string symbol, decimal price) {
            var stock = Entries.FirstOrDefault(e => e.Symbol == symbol);
            if (stock == null) return;
            
            stock.CurrentPrice = price;
            stock.LastUpdated = DateTime.Now;
        }
    }
}