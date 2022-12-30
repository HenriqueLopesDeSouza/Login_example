using Application.Api.ViewModels;
using Application.Business.Interfaces;
using Application.Business.Models;
using Application.Business.Models.Enumerators;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Application.Api.Controllers
{
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public LoginController(IUserService  userService,
                               IMapper       mapper,
                               ITokenService tokenService)
        {
            _userService = userService;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("api/auth/login")]
        [AllowAnonymous]
        public ActionResult Login(string username, string password)
        {
            try
            {
                User? user = new User();
                Result result = _userService.Login(out user, username, password);

                if (result == Result.Success)
                {

                    string message;

                    Tokens? tokenExist = _tokenService.GetRefreshToken(username:username);
                    if (tokenExist != null) 
                    {
                        Result resultDelete = _tokenService.DeleteRefreshToken(out message, username);
                        if (resultDelete != Result.Success) 
                        {
                            return BadRequest(message);
                        }
                    }

                    var token = _tokenService.GenerateToken(user);
                    var refreshToken = _tokenService.GenerateRefreshToken();

                    Result resultToken =  _tokenService.SaveRefreshToken(
                                            user.Name, refreshToken, out message);

                    if (resultToken == Result.Success)
                    {
                        return Ok(new
                        {
                            success = true,
                            token = token,
                            refreshToken = refreshToken,
                            message = "LOGGED"
                        });
                    }
                    else 
                    {
                        return BadRequest("invalid username or password");
                    }
                }
                else
                {
                    return BadRequest("invalid username or password");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/auth/register")]
        [AllowAnonymous]
        public ActionResult RegisterUser([FromBody] UserViewModel userViewModel)
        {
            try 
            {
                string message = " ";
                var user = _mapper.Map<User>(userViewModel);

                Result result = _userService.InsertUser(user, out message);

                if (result == Result.Success)
                {
                    var token = _tokenService.GenerateToken(user);
                    var refreshToken = _tokenService.GenerateRefreshToken();

                    string messageToken;

                    Result resultToken = _tokenService.SaveRefreshToken(
                                            user.Name, refreshToken, out messageToken);
                    if (resultToken == Result.Success)
                    {
                        return Ok(new
                        {
                            success = true,
                            token = token,
                            refreshToken = refreshToken,
                            message = "registered"
                        });
                    }
                    else
                    {
                        return BadRequest(messageToken);
                    }
                }
                else 
                {
                    return BadRequest(message);
                }

            } catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/auth/refresh")]
        [AllowAnonymous]
        public ActionResult Refresh([FromBody] Tokens token)
        {
            try
            {
                var principal = _tokenService.GetPrincipalFromExpiredToken(token.Token);
                var username = principal.Identity.Name;
                var savedRefreshToken = _tokenService.GetRefreshToken(token:token.Token);

                if (savedRefreshToken.Token != token.RefreshToken)
                    throw new SecurityTokenException("invalid token");

                string message;
                var newJwtToken = _tokenService.GenerateToken(principal.Claims);
                var newRefreshToken = _tokenService.GenerateRefreshToken();
                Result resultDelete = _tokenService.DeleteRefreshToken(out message,username, token.RefreshToken);
                             
                Result resultRefreshToken = _tokenService.SaveRefreshToken(username, newRefreshToken, out message);

                return new ObjectResult(new 
                {
                    token = newJwtToken,
                    refreshToken = newRefreshToken
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
