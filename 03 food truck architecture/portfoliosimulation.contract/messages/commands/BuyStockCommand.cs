using System;
using messagehandling;

namespace portfoliosimulation.contract.messages.commands
{
    public class BuyStockCommand : Command
    {
        public string StockName;
        public string StockSymbol;
        public string StockPriceCurrency;
        public int Qty;
        public decimal StockPrice;
        public DateTime Bought;
    }
}