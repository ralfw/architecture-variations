using System;

namespace portfoliosimulation.contract.messages.commands.buystock
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