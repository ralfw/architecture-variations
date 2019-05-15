using eventstore;

namespace portfoliosimulation.backend.messagepipelines.commands.sellstockcommand
{
    public class SellStockCommandManager : PortfoliosymbolsContextManager<SellStockCommandContextModel> {
        public SellStockCommandManager(IEventStore es) : base(es) {}
    }
}