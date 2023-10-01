using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;
using NovelsRanboeTranslates.Services.Interfraces;
using System.Net;

namespace NovelsRanboeTranslates.Controllers
{
    [ApiController]
    [Route("Book")]
    public class BookController : ControllerBase
    {
        private readonly ICommentsService _commentsService;
        private readonly IBookService _bookService;
        private readonly IChapterService _chapterService;
        private readonly IUserService _userService;

        public BookController(IBookService bookService, IChapterService chapterService, ICommentsService commentsService, IUserService userService)
        {
            _bookService = bookService;
            _chapterService = chapterService;
            _commentsService = commentsService;
            _userService = userService;
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
            _bookService.AddViewToBookById(bookId);
            if (result != null)
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

        [HttpGet]
        [Route("GetCommentsByBookId")]
        public async Task<IActionResult> GetCommentsByBookId(int bookId)
        {
            var result = await _commentsService.GetCommentsAsync(bookId);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("AddComments")]
        [Authorize]
        public IActionResult AddComments(CommentViewModel comment)
        {
            var newComment = new Comment(User.Claims.FirstOrDefault().Value, comment.Text, comment.Liked);
            var result = _commentsService.AddComment(comment.BookId, newComment);
            if (result != null)
            {
                var comments = _commentsService.GetCommentsAsync(comment.BookId).Result;
                int totalComments = comments.Result.Comment.Count;
                int likedCount = comments.Result.Comment.Count(c => c.Liked);
                double likedPercentage = likedCount / (double)totalComments * 100;
                _bookService.UpdateLikedPercent(comment.BookId, (int)likedPercentage);
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("GetChapterToRead")]
        public async Task<IActionResult> GetChapterToRead(ChapterToReadViewModel model)
        {
            var chapters = await _chapterService.GetChaptersAsync(model.BookId);
            if (chapters == null)
            {
                return NotFound(new Response<Chapters>("Chapter not found", null, HttpStatusCode.NotFound));
            }
            var chapterContain = chapters.Result.Chapter.Find(c => c.ChapterId == model.ChapterId);
            if (chapterContain == null)
            {
                return NotFound(new Response<Chapters>("Chapter not found", null, HttpStatusCode.NotFound));
            }
            if (chapterContain.HasPrice)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized(new Response<Chapter>("Unauthorize", null, HttpStatusCode.Unauthorized));
                }
                var user = _userService.GetUserByLogin(User.Claims.FirstOrDefault().Value);
                var purchasedBook = user.Result.Purchased.Find(u => u.BookID == model.BookId);
                var purchasedChapter = purchasedBook.ChapterID.Contains(model.ChapterId);
                if (purchasedChapter)
                {
                    return Ok(new Response<Chapter>("Correct", chapterContain, HttpStatusCode.OK));
                }
                else
                {
                    return NotFound(new Response<Chapter>("Not found in purchased", null, HttpStatusCode.NotFound));
                }
            }
            else
            {
                return Ok(new Response<Chapter>("Correct", chapterContain, HttpStatusCode.OK));
            }

        }

        [HttpGet]
        [Route("SearchBookByName")]
        public async Task<IActionResult> SearchBookByName(string bookName)
        {
            var bookList = await _bookService.SearchBookByName(bookName);
            return Ok(bookList);
        }

        [HttpPost]
        [Route("AdvancedSearch")]
        public async Task<IActionResult> AdvancedSearch(AdvancedSearchViewModel model)
        {
            var result = await _bookService.AdvancedSearch(model.OriginalLanguage, model.SortType, model.Genres, model.SkipCounter);
            return Ok(result);
        }
    }
}
