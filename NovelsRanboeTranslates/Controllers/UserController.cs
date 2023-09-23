using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;
using NovelsRanboeTranslates.Services.Interfraces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace NovelsRanboeTranslates.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBookService _bookService;
        private readonly IChapterService _chapterService;
        private readonly JWTSettings _options;
        public UserController(IUserService userService, IOptions<JWTSettings> options, IBookService bookService, IChapterService chapterService)
        {
            _userService = userService;
            _bookService = bookService;
            _options = options.Value;
            _chapterService = chapterService;
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
        [Route("GetUserByLogin")]
        [Authorize]
        public IActionResult GetUserByLogin(string login)
        {
            if (login == User.Claims.FirstOrDefault().Value)
            {
                var user = _userService.GetUserByLogin(login);
                return Ok(user);
            }
            return Ok();
        }

        [HttpGet]
        [Route("GetUserByToken")]
        [Authorize]
        public IActionResult GetUserByToken()
        {
            try
            {
                var user = _userService.GetUserByLogin(User.Claims.FirstOrDefault().Value);
                return Ok(user);
            }
            catch
            {
                return Ok(new Response<User>("Something wrong with token", null, System.Net.HttpStatusCode.NotFound));
            }
        }

        [HttpPost]
        [Route("BuyChapter")]
        [Authorize]
        public async Task<IActionResult> BuyChapter(BuyChapterViewModel model)
        {
            var responseBook = await _bookService.GetBookByIdAsync(model.BookId);
            if (responseBook.Result == null)
            {
                return Ok(new Response<bool>(responseBook.Comment, false, System.Net.HttpStatusCode.NotFound));
            }
            var book = responseBook.Result;
            var chapters = await _chapterService.GetChaptersDTOAsync(model.BookId);
            if (chapters == null)
            {
                return Ok(new Response<bool>(chapters.Comment, false, System.Net.HttpStatusCode.NotFound));
            }
            var existingChapter = chapters.Result.Chapter.FirstOrDefault(c => c.ChapterId == model.ChapterId);
            if (existingChapter == null)
            {
                return Ok(new Response<bool>("Chapter not found", false, System.Net.HttpStatusCode.NotFound));
            }
            var result = _userService.AddPurchased(model, User.Claims.FirstOrDefault().Value, existingChapter.Price);
            return Ok(result);
        }

        [HttpPost]
        [Route("AddToBalance")]
        [Authorize("Admin")]
        public async Task<IActionResult> AddToBalance(string login, decimal value)
        {
            var user = _userService.GetBaseUserByLogin(login);
            if (user.Result == null) return BadRequest("User Not found");
            var addOperation = await _userService.AddToBalance(login, value);
            if (addOperation) return Ok("Success");
            return BadRequest("Something wrong with add operation");
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
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
