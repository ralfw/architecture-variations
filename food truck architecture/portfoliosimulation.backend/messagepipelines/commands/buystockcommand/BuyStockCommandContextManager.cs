using System.Collections.Generic;
using eventstore;
using messagehandling;
using messagehandling.pipeline.messagecontext;

namespace portfoliosimulation.backend.messagepipelines.commands.buystockcommand
{
    public class BuyStockCommandContextManager : IMessageContextManager
    {
        public IMessageContext Load(IMessage input) => new BuyStockCommandContextModel();

        public void Update(IEnumerable<Event> events) { }
    }
}