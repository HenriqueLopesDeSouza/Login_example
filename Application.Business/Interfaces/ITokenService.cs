using Application.Business.Models;
using Application.Business.Models.Enumerators;
using System.Security.Claims;


namespace Application.Business.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);

        public string GenerateToken(IEnumerable<Claim> claims);

        public string GenerateRefreshToken();
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

        public Result SaveRefreshToken(string username, string refreshToken, out string message);

        public Tokens? GetRefreshToken(string? username = null, string? token = null);

        public Result DeleteRefreshToken(out string message,string? username = null, string? refreshToken = null);



    }
}
