using System;
using eventstore;

namespace portfoliosimulation.backend.events
{
    public class StockSold : Event
    {
        public string Symbol;
    }
}