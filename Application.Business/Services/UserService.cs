using Application.Business.Interfaces;
using Application.Business.Models;
using Application.Business.Models.Enumerators;

namespace Application.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;


        public UserService(IUserRepository personRepository, IPasswordService passwordService)
        {
            _userRepository = personRepository;
            _passwordService = passwordService;
        }

        public Result Login(out User? user , string userName, string password)
        {
                user = _userRepository.Login(userName);

            if (user != null)
            {
                int result = 0;
                byte[] salt =  StringToByteArray(user.Salt);
                bool check = _passwordService.VerifyPassword(password, user.Password, salt);
                
                if (check) 
                {
                    result = 1;
                    return (Result)result;
                }
                result = -1;
                user = null;
                return (Result)result;

            }
            else 
            {
                int result = -1;
                user = null;
                return (Result)result;
            }
        }

        public List<User> Get()
        {
            return _userRepository.Get();
        }

        public Result InsertUser(User user, out string? message)
        {
            byte[] salt;
            user.Password = _passwordService.HashPasword(user.Password,out salt);
            user.Salt = Convert.ToHexString(salt);
            return _userRepository.InsertUser(user,out message);
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
