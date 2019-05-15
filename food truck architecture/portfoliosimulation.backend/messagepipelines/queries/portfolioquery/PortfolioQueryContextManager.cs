using System.Collections.Generic;
using System.Linq;
using eventstore;
using messagehandling;
using messagehandling.pipeline.messagecontext;
using portfoliosimulation.backend.events;

namespace portfoliosimulation.backend.messagepipelines.queries.portfolioquery
{
    public class PortfolioQueryContextManager : IMessageContextManager {
        private readonly Dictionary<string, PortfolioQueryContextModel.StockInfo> _portfolio;
        
        public PortfolioQueryContextManager(IEventStore es)  {
            _portfolio = new Dictionary<string, PortfolioQueryContextModel.StockInfo>();
            Update(es.Replay(typeof(StockBought), typeof(StockSold), typeof(StockPriceUpdated)));
        }

        
        public IMessageContext Load(IMessage _) => new PortfolioQueryContextModel{Entries = _portfolio.Values};

        
        public void Update(IEnumerable<Event> events)
            => events.ToList().ForEach(Apply);

        private void Apply(Event e) {
            switch (e) {
                case StockBought sb: {
                    if (_portfolio.TryGetValue(sb.Symbol, out var stockinfo)) {
                        stockinfo.Qty += sb.Qty;
                        stockinfo.BuyingPrice = (stockinfo.BuyingPrice + sb.Price) / 2.0m;
                    }
                    else {
                        stockinfo = new PortfolioQueryContextModel.StockInfo {
                            Name = sb.Name,
                            Symbol = sb.Symbol,
                            Currency = sb.Currenncy,
                            Qty = sb.Qty,
                            BuyingPrice = sb.Price,
                            Bought = sb.Timestamp
                        };
                        _portfolio.Add(sb.Symbol, stockinfo);
                    }
                    break;
                }
                case StockSold ss:
                    _portfolio.Remove(ss.Symbol);
                    break;
                case StockPriceUpdated spu: {
                    var stockinfo = _portfolio[spu.Symbol];
                    stockinfo.CurrentPrice = spu.Price;
                    stockinfo.LastUpdated = spu.Timestamp;
                    break;
                }
            }
        }
    }
}