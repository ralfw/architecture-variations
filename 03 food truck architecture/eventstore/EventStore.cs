using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace eventstore
{
    public class EventStore : IEventStore
    {
        private readonly string _path;

        public event Action<Event[]> OnRecorded = _ => { };
        
        public EventStore(string path) {
            _path = path;
            if (Directory.Exists(_path) is false)
                Directory.CreateDirectory(_path);
        }

        
        public void Record(Event e) => Record(new[] {e});
        public void Record(Event[] events) {
            var index = Directory.GetFiles(_path).Length;
            events.ToList().ForEach(e => Store(e, ++index));
            OnRecorded(events);
        }


        public IEnumerable<Event> Replay()
            => Directory.GetFiles(_path, "*.txt")
                .OrderBy(p => p)
                .Select(Load);

        public IEnumerable<Event> Replay(params Type[] eventTypes) {
            var eventTypes_ = new HashSet<Type>(eventTypes);
            return Replay().Where(e => eventTypes_.Contains(e.GetType()));
        }

        
        private void Store(Event e, long index) {
            var text = EventSerialization.Serialize(e);
            Write(text, index);
        }
        
        private void Write(string e, long index) {
            var filepath = Path.Combine(_path, $"{index:D8}.txt");
            File.WriteAllText(filepath, e);
        }
        
        
        private Event Load(string filename) {
            var text = File.ReadAllText(filename);
            return EventSerialization.Deserialize(text);
        }

        
        public void Dispose(){}
    }
}