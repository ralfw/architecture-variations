using System.Collections.Generic;
using eventstore;
using messagehandling;
using messagehandling.pipeline.messagecontext;

namespace portfoliosimulation.backend.messagepipelines.queries.candidatestocksquery
{
    public class CandidateStocksQueryContextManager : IMessageContextManager {
        public IMessageContext Load(IMessage input) => new CandidatStocksQueryContextModel();

        public void Update(IEnumerable<Event> events) { }
    }
}