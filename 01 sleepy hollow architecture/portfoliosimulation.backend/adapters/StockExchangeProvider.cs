using System;
using System.Collections.Generic;
using System.Linq;
using onlinestocks.alphavantage;
using portfoliosimulation.contract;

namespace portfoliosimulation.backend.adapters
{
    class StockExchangeProvider : IStockExchangeProvider
    {
        private readonly IAlphaVantageProvider _av;

        public StockExchangeProvider() : this(new AlphaVantageProvider()) {}
        internal StockExchangeProvider(IAlphaVantageProvider av) { _av = av; }
        
        
        public (string Symbol, decimal Price)[] GetPrice(params string[] symbols) {
            var prices = new List<(string,decimal)>();
            foreach (var symbol in symbols) {
                try {
                    var quote = _av.GetQuote(symbol);
                    prices.Add((symbol, (decimal)quote.Price));
                }
                catch {
                    prices.Add((symbol, 0.0m));
                }
            }
            return prices.ToArray();
        }


        public CandidateStockInfo[] FindCandidates(string pattern)  {
            var stockInfos = _av.FindMatchingStocks(pattern);
            var stockPrices = Compile_prices(stockInfos.Select(x => x.Symbol));
            var candidates = stockInfos.Zip(stockPrices, (si, p) => new CandidateStockInfo {
                Name = si.Name,
                Symbol = si.Symbol,
                Currency = si.Currency,
                Price = p
            });
            return candidates.Where(c => c.Price > 0.0m).ToArray();


            decimal[] Compile_prices(IEnumerable<string> symbols)
                => symbols.Select(GetQuote).ToArray();

            decimal GetQuote(string symbol) {
                try {
                    return (decimal) _av.GetQuote(symbol).Price;
                }
                catch {
                    return 0.0m;
                }
            }
        }
    }
}