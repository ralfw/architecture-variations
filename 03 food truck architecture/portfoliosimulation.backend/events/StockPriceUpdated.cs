using System;
using eventstore;

namespace portfoliosimulation.backend.events
{
    public class StockPriceUpdated : Event
    {
        public string Symbol;
        public decimal Price;
    }
}