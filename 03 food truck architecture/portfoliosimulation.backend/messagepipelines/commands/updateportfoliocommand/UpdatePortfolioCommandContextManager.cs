using System.Collections.Generic;
using eventstore;
using messagehandling;
using messagehandling.pipeline.messagecontext;
using portfoliosimulation.backend.messagepipelines.commands.sellstockcommand;

namespace portfoliosimulation.backend.messagepipelines.commands.updateportfoliocommand
{
    public class UpdatePortfolioCommandContextManager : PortfoliosymbolsContextManager<UpdatePortfolioCommandContextModel> {
        public UpdatePortfolioCommandContextManager(IEventStore es) : base(es) {}
    }
}