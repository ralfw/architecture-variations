using Newtonsoft.Json;

namespace eventstore
{
    static class EventSerialization
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.All
        };
        public static string Serialize(Event e) {
            return JsonConvert.SerializeObject(e, Settings);
        }

        public static Event Deserialize(string e) {
            return (Event) JsonConvert.DeserializeObject(e, Settings);
        }
    }
}