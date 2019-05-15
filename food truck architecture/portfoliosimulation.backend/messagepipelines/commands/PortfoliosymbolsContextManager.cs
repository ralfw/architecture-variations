using System;
using System.Collections.Generic;
using System.Linq;
using eventstore;
using messagehandling;
using messagehandling.pipeline.messagecontext;
using portfoliosimulation.backend.events;

namespace portfoliosimulation.backend.messagepipelines.commands
{
    public class PortfoliosymbolsContextManager<T> : IMessageContextManager where T : PortfoliosymbolsContextModel
    {
        private readonly HashSet<string> _symbolsInPortfolio;

        protected PortfoliosymbolsContextManager(IEventStore es) {
            _symbolsInPortfolio = new HashSet<string>();
            Update(es.Replay(typeof(StockBought), typeof(StockSold)));
        }
        
        public IMessageContext Load(IMessage input) {
            return (T) Activator.CreateInstance(typeof(T), new HashSet<string>(_symbolsInPortfolio));
        }

        public void Update(IEnumerable<Event> events) => events.ToList().ForEach(Apply);

        private void Apply(Event e) {
            switch (e) {
                case StockBought sb:
                    _symbolsInPortfolio.Add(sb.Symbol);
                    break;
                case StockSold ss:
                    _symbolsInPortfolio.Remove(ss.Symbol);
                    break;
            }
        }
    }
}