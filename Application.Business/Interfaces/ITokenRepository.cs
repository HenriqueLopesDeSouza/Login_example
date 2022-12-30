using Application.Business.Models;
using Application.Business.Models.Enumerators;

namespace Application.Business.Interfaces
{
    public interface ITokenRepository
    {
        public Tokens? GetToken(string? username = null, string? token = null);

        public Result InsertToken(string username, string token, out string message);

        public Result DeleteToken(out string message, string? username = null, string? refreshToken = null);

    }
}
