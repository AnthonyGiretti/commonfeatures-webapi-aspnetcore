namespace WebApiDemo.Services
{
    public interface IJsonSerializationService
    {
        string Serialize(object objectToSerialize);

        T Deserialize<T>(string stringToSerialize);
    }
}