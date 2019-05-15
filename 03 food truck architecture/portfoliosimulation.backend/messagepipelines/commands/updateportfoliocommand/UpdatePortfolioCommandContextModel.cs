using System.Collections.Generic;
using messagehandling.pipeline.messagecontext;
using portfoliosimulation.backend.messagepipelines.commands.sellstockcommand;

namespace portfoliosimulation.backend.messagepipelines.commands.updateportfoliocommand
{
    public class UpdatePortfolioCommandContextModel : PortfoliosymbolsContextModel {
        public UpdatePortfolioCommandContextModel(HashSet<string> values) : base(values) {}
    }
}