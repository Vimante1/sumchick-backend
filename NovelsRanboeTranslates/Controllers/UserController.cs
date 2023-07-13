using Microsoft.AspNetCore.Mvc;
using NovelsRanboeTranslates.Services.Interfraces;
using NovelsRanboeTranslates.Services.Services;

namespace NovelsRanboeTranslates.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUser(string login) {

            var user = _userService.GetUserByLogin(login);
            return Ok(user);
        }
    }
}
