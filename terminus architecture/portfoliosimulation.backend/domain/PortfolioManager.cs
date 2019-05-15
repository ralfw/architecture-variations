using System.Collections.Generic;
using System.Linq;
using portfoliosimulation.contract.data.domain;

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
        public static PortfolioReturns CalculateReturns(Portfolio portfolio)
        {
            var returns = portfolio.Entries.Aggregate(new Dictionary<string, PortfolioReturns.EntryReturn>(),
                (rs, e) => {
                    var buyingValue = e.Qty * e.BuyingPrice;
                    var r = new PortfolioReturns.EntryReturn {
                        Return = e.Qty * e.CurrentPrice - buyingValue
                    };
                    r.RateOfReturn = r.Return / buyingValue;
                    rs[e.Symbol] = r;
                    return rs;
                });

            var pr = new PortfolioReturns {
                Returns = returns,
                TotalReturn = returns.Values.Sum(v => v.Return)
            };
            var totalBuyingValue = portfolio.Entries.Sum(e => e.Qty * e.BuyingPrice);

            pr.TotalRateOfReturn = totalBuyingValue > 0.0m ? pr.TotalReturn / totalBuyingValue : 0.0m;
            
            return pr;
        }
    }
}