using System;
using System.Collections.Generic;

namespace eventstore
{
    public interface IEventStore : IDisposable
    {
        event Action<Event[]> OnRecorded;
        void Record(Event e);
        void Record(Event[] events);
        IEnumerable<Event> Replay();
        IEnumerable<Event> Replay(params Type[] eventTypes);
    }
}