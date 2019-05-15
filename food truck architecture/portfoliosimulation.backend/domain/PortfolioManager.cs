using System.Collections.Generic;
using System.Linq;

namespace portfoliosimulation.backend.domain
{
    class PortfolioReturns
    {
        public class EntryReturn {
            public decimal Return;
            public decimal RateOfReturn;
        }

        public Dictionary<string, EntryReturn> Returns = new Dictionary<string, EntryReturn>();
        
        public decimal TotalReturn;
        public decimal TotalRateOfReturn;
    }
    
    
    class PortfolioManager
    {
        public static PortfolioReturns CalculateReturns((string symbol, int qty, 
                                                         decimal buyingPrice, decimal currentPrice)[] portfolio)
        {
            var returns = portfolio.Aggregate(new Dictionary<string, PortfolioReturns.EntryReturn>(),
                (rs, e) => {
                    var buyingValue = e.qty * e.buyingPrice;
                    var r = new PortfolioReturns.EntryReturn {
                        Return = e.qty * e.currentPrice - buyingValue
                    };
                    r.RateOfReturn = r.Return / buyingValue;
                    rs[e.symbol] = r;
                    return rs;
                });

            var pr = new PortfolioReturns {
                Returns = returns,
                TotalReturn = returns.Values.Sum(v => v.Return)
            };
            var totalBuyingValue = portfolio.Sum(e => e.qty * e.buyingPrice);

            pr.TotalRateOfReturn = totalBuyingValue > 0.0m ? pr.TotalReturn / totalBuyingValue : 0.0m;
            
            return pr;
        }
    }
}