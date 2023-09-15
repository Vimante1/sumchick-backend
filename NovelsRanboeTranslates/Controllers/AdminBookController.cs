using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;
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

        public AdminBookController(IBookService bookService, IChapterService chapterService)
        {
            _bookService = bookService;
            _chapterService = chapterService;
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
                        return Ok(_bookService.CreateNewBook(book, "http://158.101.181.252/images/" + fileName));
                    }
                    else
                    {
                        return BadRequest("Зображення не було завантажено");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }

            }
            return Ok(ModelState);
        }

        [HttpPost]
        [Route("CreateChapter")]
        public IActionResult CreateChapter(int bookId, Chapter chapter)
        {
            if (!ModelState.IsValid) return Ok(ModelState);
            var result = _chapterService.AddChapter(bookId, chapter);
            return Ok(result);
        }
    }
}
