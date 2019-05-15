using System;

namespace eventstore
{
    public abstract class Event {
        public string Id { get; set; }
        public DateTime Timestamp { get; set; }

        public Event() {
            Id = Guid.NewGuid().ToString();
            Timestamp = DateTime.Now;
        }
    }
}