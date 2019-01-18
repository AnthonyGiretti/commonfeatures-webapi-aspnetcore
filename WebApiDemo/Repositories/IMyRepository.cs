using WebApiDemo.Models;

namespace WebApiDemo.Repositories
{
    public interface IMyRepository
    {
        UserEntity GetUserById(int id);
    }
}