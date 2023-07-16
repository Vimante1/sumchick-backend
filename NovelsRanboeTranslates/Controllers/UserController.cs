using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;
using NovelsRanboeTranslates.Services.Interfraces;
using NovelsRanboeTranslates.Services.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZstdSharp.Unsafe;

namespace NovelsRanboeTranslates.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JWTSettings _options;
        public UserController(IUserService userService, IOptions<JWTSettings> options)
        {
            _userService = userService;
            _options = options.Value;
        }

        [HttpPost]
        [Route("Registration")]
        public IActionResult Registration(RegistrationViewModel credentials)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.CreateNewUser(credentials);
                if (user.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(new Response<string>("Correct", GetToken(user.Result), System.Net.HttpStatusCode.OK));
                }
                return Ok(user);
            }
            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(AuthorizationViewModel credentials)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.Login(credentials);
                if (user.Result != null) return Ok(new Response<string>("Correct", GetToken(user.Result), System.Net.HttpStatusCode.OK));
                else return Ok(user);
            }
            return Ok();
        }

        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUser(string login)
        {

            var user = _userService.GetUserByLogin(login);
            return Ok(user);
        }

        private string GetToken(User user)
        {
            List<Claim> claims = new();
            claims.Add(new Claim(ClaimTypes.Name, user.Login));
            claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(120)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }
    }
}
