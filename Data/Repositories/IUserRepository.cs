using backend.Models;

namespace backend.Controllers.Repositories
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetUserByEmail(string email);
        User GetById(int id);
    }
}
