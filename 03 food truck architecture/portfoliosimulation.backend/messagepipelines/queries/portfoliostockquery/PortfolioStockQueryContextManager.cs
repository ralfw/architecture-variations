using System;
using System.Collections.Generic;
using System.Linq;
using eventstore;
using messagehandling;
using messagehandling.pipeline.messagecontext;
using portfoliosimulation.backend.events;
using portfoliosimulation.contract.messages.queries.portfoliostock;

namespace portfoliosimulation.backend.messagepipelines.queries.portfoliostockquery
{
    public class PortfolioStockQueryContextManager : IMessageContextManager
    {
        private readonly Dictionary<string, string> _portfolio;
        
        public PortfolioStockQueryContextManager(IEventStore es) {
            _portfolio = new Dictionary<string, string>();
            Update(es.Replay(typeof(StockBought), typeof(StockSold)));
        }
        
        
        public IMessageContext Load(IMessage input) {
            var query = input as PortfolioStockQuery;
            
            var matchingStocks = _portfolio.Aggregate(
                new List<(string Name, string Symbol)>(),
                (ms, e) => {
                    if (e.Key.IndexOf(query.Pattern, StringComparison.InvariantCultureIgnoreCase) >= 0 || 
                        e.Value.IndexOf(query.Pattern, StringComparison.InvariantCultureIgnoreCase) >= 0)
                        ms.Add((e.Value, e.Key));
                    return ms;
                });
            
            return new PortfolioStockQueryContextModel  {
                MatchingStocks = matchingStocks.ToArray()
            };
        }


        public void Update(IEnumerable<Event> events) => events.ToList().ForEach(Apply);

        private void Apply(Event e)  {
            switch (e) {
                case StockBought sb:
                    _portfolio[sb.Symbol] = sb.Name;
                    break;
                case StockSold ss:
                    _portfolio.Remove(ss.Symbol);
                    break;
            }
        }
    }
}