using System.Collections.Generic;
using eventstore;

namespace messagehandling.pipeline.messagecontext
{
    public interface IMessageContextBuilder {
        void Update(IEnumerable<Event> events);
    }
}