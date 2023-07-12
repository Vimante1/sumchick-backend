using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;
using NovelsRanboeTranslates.Repository.Interfaces;
using NovelsRanboeTranslates.Services.Interfraces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NovelsRanboeTranslates.Controllers
{
    [ApiController]
    [Route("Registration")]
    public class RegistrationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JWTSettings _options;
        public RegistrationController(IUserService userService, IOptions<JWTSettings> options)
        {
            _userService = userService;
            _options = options.Value;
        }

        [HttpPost]
        [Route("CreateUser")]
        public IActionResult CreateUser(RegistrationViewModel user)
        {
            if (ModelState.IsValid)
            {
                var newUser = _userService.CreateNewUser(user);
                if (newUser.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(GetToken(newUser.Result));
                }
            }
            return Ok(user);
            
        }


        private string GetToken(User user)
        {
            List<Claim> claims = new List<Claim>();
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