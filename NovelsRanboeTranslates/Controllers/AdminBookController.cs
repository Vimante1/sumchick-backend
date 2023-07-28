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

        public AdminBookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        [Route("CreateNewBook")]
        public IActionResult CreateNewBook([FromForm] CreateNewBookViewModel book, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (image != null && image.Length > 0)
                    {
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

                        var imagePath = Path.Combine("D:/images/", fileName);

                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            image.CopyTo(stream);
                        }
                        return Ok(_bookService.CreateNewBook(book, imagePath));
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
        [Route("AddChapter")]
        public IActionResult AddChapter(int bookId, [FromBody] AddChapterViewModel model)
        {
            if (model != null && bookId != 0 && ModelState.IsValid)
            {
                var result = _bookService.AddChapterToBook(bookId, model);
                return Ok(result);
            }
            return Ok(new Response<bool>("input id 0", false, System.Net.HttpStatusCode.NoContent)) ;
        }
    }
}
