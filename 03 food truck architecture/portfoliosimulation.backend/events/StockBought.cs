using System;
using eventstore;

namespace portfoliosimulation.backend.events
{
    public class StockBought : Event
    {
        public string Name;
        public string Symbol;
        public string Currenncy;
        public int Qty;
        public decimal Price;
    }
}