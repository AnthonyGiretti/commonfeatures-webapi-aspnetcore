using System.Text.Json;

namespace WebApiDemo.Services
{
    public class JsonSerializerService : IJsonSerializationService
    {
        public string Serialize(object objectToSerialize)
        {
            return JsonSerializer.Serialize(objectToSerialize);
        }

        public T Deserialize<T>(string stringToSerialize)
        {
            return JsonSerializer.Deserialize<T>(stringToSerialize);
        }
    }
}