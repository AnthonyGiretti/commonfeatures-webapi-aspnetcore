namespace WebApiDemo.Providers
{
    public interface IServicesProvider<TInterface>
    {
        TInterface GetInstance(string key);
    }
}
