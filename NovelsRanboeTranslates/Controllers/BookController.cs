using Microsoft.AspNetCore.Mvc;
using NovelsRanboeTranslates.Services.Interfraces;

namespace NovelsRanboeTranslates.Controllers
{
    [ApiController]
    [Route("Book")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Route("GetCompilation")]
        public IActionResult GetCompilation()
        {
            var compilation = _bookService.GetBooksCompilation();

            return Ok(compilation);
        }
    }
}
