using Newtonsoft.Json;
using System;
using System.Text;

namespace ArganaWeedApp.Services
{
    public class ServiceSerializer<T> where T : class
    {
        public static T Deserialize(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException(nameof(data), "Input data for deserialization cannot be null or empty");
            }
            //Console.WriteLine("************Debut deserialisation **************");
            //Console.WriteLine("Deserializing data: " + data); // Add this line to log the data
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static T DeserializeFromBytes(byte[] data)
        {
            return Deserialize(Encoding.UTF8.GetString(data, 0, data.Length));
        }

        public static string Serialize(T obj)
        {
            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        public static byte[] SerializeToBytes(T obj)
        {
            return Encoding.UTF8.GetBytes(Serialize(obj));
        }

        public static byte[] ToBytes(string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }
    }
}
