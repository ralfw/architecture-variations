using System;

namespace portfoliosimulation.contract.data.messages.commands
{
    public class BuyStockCommand
    {
        public string StockName;
        public string StockSymbol;
        public string StockPriceCurrency;
        public int Qty;
        public decimal StockPrice;
        public DateTime Bought;
    }
}