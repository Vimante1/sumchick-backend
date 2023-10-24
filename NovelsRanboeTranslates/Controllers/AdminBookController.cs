using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;
using NovelsRanboeTranslates.Services.Interfaces;
using NovelsRanboeTranslates.Services.Interfraces;

namespace NovelsRanboeTranslates.Controllers
{
    [ApiController]
    [Route("AdminBook")]
    [Authorize("Admin")]
    public class AdminBookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IChapterService _chapterService;
        private readonly IBookStatisticService _bookStatistic;

        public AdminBookController(IBookService bookService, IChapterService chapterService, IBookStatisticService bookStatistic)
        {
            _bookService = bookService;
            _chapterService = chapterService;
            _bookStatistic = bookStatistic;
        }

        [HttpPost]
        [Route("CreateNewBook")]
        public IActionResult CreateNewBook([FromForm] CreateNewBookViewModel book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (book.Image != null && book.Image.Length > 0)
                    {
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(book.Image.FileName)}";

                        var imagePath = Path.Combine("/app/images/", fileName);

                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            book.Image.CopyTo(stream);
                        }
                        return Ok(_bookService.CreateNewBook(book, "https://udovychenko.site/images/" + fileName));
                    }
                    else
                    {
                        return BadRequest("Image not upload");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }

            }
            return Ok(ModelState);
        }

        [HttpPut]
        [Route("UpdateBook")]
        public async Task<IActionResult> UpdateBook(UpdateBookViewModel updateBook)
        {
            return Ok(await _bookService.UpdateBook(updateBook));
        }

        [HttpPut]
        [Route("UpdateOneChapter")]
        public async Task<IActionResult> UpdateOneChapter(int bookId, Chapter updateChapter)
        {
            return Ok(await _chapterService.UpdateOneChapter(bookId, updateChapter));
        }

        [HttpPost]
        [Route("CreateChapter")]
        public IActionResult CreateChapter(int bookId, Chapter chapter)
        {
            if (!ModelState.IsValid) return Ok(ModelState);
            var result = _chapterService.AddChapter(bookId, chapter);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetBookStatisticList")]
        public async Task<IActionResult> GetBookStatisticList()
        {
            return Ok(await _bookStatistic.GetStatisticList());
        }

        [HttpGet]
        [Route("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await _bookService.GetAllBooks());
        }
    }
}
