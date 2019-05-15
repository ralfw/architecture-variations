using eventstore;

namespace messagehandling.pipeline
{
    public class CommandOutput : Output {
        public CommandStatus Status { get; }
        public Event[] Events { get; }

        
        public CommandOutput(Success status, Event[] events) {
            Status = status;
            Events = events;
        }

        public CommandOutput(Failure failure) {
            Status = failure;
            Events = new Event[0];
        }
    }
}