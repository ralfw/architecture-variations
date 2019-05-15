using System.Collections.Generic;
using messagehandling.pipeline.messagecontext;

namespace portfoliosimulation.backend.messagepipelines.commands
{
    public class PortfoliosymbolsContextModel : IMessageContext {
        public PortfoliosymbolsContextModel(HashSet<string> values) => Values = values;
        
        public HashSet<string> Values { get; private set; }
    }
}