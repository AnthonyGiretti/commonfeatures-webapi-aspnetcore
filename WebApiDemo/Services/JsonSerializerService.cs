//using Newtonsoft.Json;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApiDemo.Services
{

    //public class NewtonsoftSerializerService 
    //{
    //    public T DeSerialize<T>(string stringObject)
    //    {
    //        return JsonConvert.DeserializeObject<T>(stringObject);
    //    }

    //    public string Serialize<T>(T objectToSerialize)
    //    {
    //        return JsonConvert.SerializeObject(objectToSerialize);
    //    }
    //}

    public class JsonSerializerService
    {
        public T DeSerialize<T>(string stringObject)
        {
            return JsonSerializer.Deserialize<T>(stringObject);
        }

        public string Serialize<T>(T objectToSerialize)
        {
            throw new NotImplementedException();
        }
    }
}