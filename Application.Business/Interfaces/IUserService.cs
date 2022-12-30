using Application.Business.Models;
using Application.Business.Models.Enumerators;

namespace Application.Business.Interfaces
{
    public interface IUserService
    {
        public List<User> Get();

        public Result InsertUser(User user, out string? message);

        public Result Login(out User? user, string userName, string password);
    }
}
