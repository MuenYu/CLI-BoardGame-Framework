using Newtonsoft.Json;

namespace Game
{
    // A tool for Serialization
    internal class SerUtil
    {
        private static readonly JsonSerializerSettings options = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented
        };

        // convert an object to json
        public static string ToJson(Object obj)
        {
            return JsonConvert.SerializeObject(obj, options);
        }

        // convert a json to type T
        public static T ToObj<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, options);
        }
    }
}
