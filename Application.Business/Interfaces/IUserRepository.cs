using Application.Business.Models;
using Application.Business.Models.Enumerators;

namespace Application.Business.Interfaces
{
    public interface IUserRepository
    {

        public List<User> Get();

        public Result InsertUser(User user, out string? message);

        public User? Login(string userName);


    }
}
