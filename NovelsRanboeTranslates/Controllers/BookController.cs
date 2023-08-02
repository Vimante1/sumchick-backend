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

        public BookController(IBookService bookService, IChapterService chapterService)
        {
            _bookService = bookService;
            _chapterService = chapterService;
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
        public async Task<IActionResult> GetBookById(int bookId)
        {
            var result = await _bookService.GetBookByIdAsync(bookId);

            if (result != null )
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetChaptersById")]
        public async Task<IActionResult> GetChaptersById(int bookId)
        {
            var result = await _chapterService.GetChaptersDTOAsync(bookId);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
