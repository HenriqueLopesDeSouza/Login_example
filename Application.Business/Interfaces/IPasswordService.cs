

namespace Application.Business.Interfaces
{
    public interface IPasswordService
    {
        public string HashPasword(string password, out byte[] salt);

        public bool VerifyPassword(string password, string hash, byte[] salt);

    }
}
