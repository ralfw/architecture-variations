using System.Collections.Generic;

namespace portfoliosimulation.backend.messagepipelines.commands.sellstockcommand
{
    public class SellStockCommandContextModel : PortfoliosymbolsContextModel {
        public SellStockCommandContextModel(HashSet<string> values) : base(values) {}
    }
}