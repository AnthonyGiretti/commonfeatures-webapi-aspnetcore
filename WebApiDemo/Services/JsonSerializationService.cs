using Newtonsoft.Json;

namespace WebApiDemo.Services
{
    public class NewtonSoftSerializationService : IJsonSerializationService
    {
        public string Serialize(object objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize);
        }

        public T Deserialize<T>(string stringToSerialize)
        {
            return JsonConvert.DeserializeObject<T>(stringToSerialize);
        }
    }
}