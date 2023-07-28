using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;
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
        [Route("GetBestBooksByGenre")]
        public IActionResult GetBestBooksByGenre() {

            var books = _bookService.GetBestBooksByGenre();
            return Ok(books);
        }
        
        [HttpGet]
        [Route("GetLatestBooks")]
        public IActionResult GetLatestBooks() {

            var books = _bookService.GetLatestBooks();
            return Ok(books);
        }

        [HttpGet]
        [Route("GetBookById")]
        public IActionResult GetBookById(int bookId)
        {
            return Ok(_bookService.GetBookById(bookId));
        }

        [HttpPost]
        [Route("AddCommentToBook")]
        public IActionResult AddCommentToBook(int bookId, Comment comment)
        {
            if (bookId != 0 && comment != null)
            {
                var result = _bookService.AddCommentToBook(bookId, comment);
                return Ok(result);
            }
            return Ok(new Response<bool>("input id 0", false, System.Net.HttpStatusCode.NoContent));
        }

        
    }
}
