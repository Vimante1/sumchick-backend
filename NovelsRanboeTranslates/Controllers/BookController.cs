using Microsoft.AspNetCore.Mvc;
using NovelsRanboeTranslates.Services.Interfraces;

namespace NovelsRanboeTranslates.Controllers
{
    [ApiController]
    [Route("Book")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IChapterService _chapterService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Route("GetBestBooksByGenre")]
        public IActionResult GetBestBooksByGenre()
        {
            var books = _bookService.GetBestBooksByGenre();
            return Ok(books);
        }

        [HttpGet]
        [Route("GetLatestBooks")]
        public IActionResult GetLatestBooks()
        {

            var books = _bookService.GetLatestBooks();
            return Ok(books);
        }

        [HttpGet]
        [Route("GetBookById")]
        public IActionResult GetBookById(int bookId)
        {
            return Ok(_bookService.GetBookById(bookId));
        }
    }
}
