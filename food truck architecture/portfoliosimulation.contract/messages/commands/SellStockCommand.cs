using messagehandling;

namespace portfoliosimulation.contract.messages.commands
{
    public class SellStockCommand : Command
    {
        public string StockSymbol;
    }
}