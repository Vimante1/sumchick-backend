using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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

        [HttpGet]
        [Route("GetBookById")]
        public IActionResult GetBookById(int bookId)
        {
            return Ok(_bookService.GetBookById(bookId));
        }
    }
}
