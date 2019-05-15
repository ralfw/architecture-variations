using System;
using System.Collections.Generic;
using messagehandling.pipeline.messagecontext;

namespace portfoliosimulation.backend.messagepipelines.queries.portfolioquery
{
    public class PortfolioQueryContextModel : IMessageContext
    {
        public class StockInfo {
            public string Name;
            public string Symbol;
            public string Currency;
            public int Qty;
            public decimal BuyingPrice;
            public DateTime Bought;
            public decimal CurrentPrice;
            public DateTime LastUpdated;
        }

        public IEnumerable<StockInfo> Entries;
    }
}