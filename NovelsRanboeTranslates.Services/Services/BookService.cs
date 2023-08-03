using NovelsRanboeTranslates.Domain.Lists;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;
using NovelsRanboeTranslates.Repository.Interfaces;
using NovelsRanboeTranslates.Services.Interfraces;

namespace NovelsRanboeTranslates.Services.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public Response<bool> CreateNewBook(CreateNewBookViewModel book, string imagePath)
        {
            Book NewBook = new Book(book.Title, book.Description, book.Author, book.OriginalLanguage, book.Genre, imagePath);
            return _repository.Create(NewBook);
        }
        public Response<List<Book>> GetBestBooksByGenre()
        {
            try
            {
                var genres = new Genres().GetList();
                var books = _repository.GetBestBooksByGenreAsync(genres);
                return new Response<List<Book>>("Correct", books.Result, System.Net.HttpStatusCode.OK);
            }
            catch
            {
                return new Response<List<Book>>("Something wrong with get from DB", null, System.Net.HttpStatusCode.BadRequest);
            }
        }
        public Response<List<Book>> GetLatestBooks()
        {
            try
            {
                var books = _repository.GetLatestBooksAsync();
                return new Response<List<Book>>("Correct", books.Result, System.Net.HttpStatusCode.OK);
            }
            catch
            {
                return new Response<List<Book>>("Something wrong with get from DB", null, System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<Response<Book>> GetBookByIdAsync(int bookId)
        {
            var book = await _repository.GetBookByIdAsync(bookId);

            if (book != null)
            {
                return new Response<Book>("Correct", book, System.Net.HttpStatusCode.OK);
            }
            return new Response<Book>("BookNotFound", null, System.Net.HttpStatusCode.NotFound);
        }

        public async Task<bool> UpdateLikedPercent(int bookId, int likedPercent)
        {
            var book = await _repository.GetBookByIdAsync(bookId);
            if (book != null)
            {
                await _repository.UpdateLikedPercentBookAsync(book, likedPercent);
                return true;
            }
            return false;
        } 

    }

}




//public Response<bool> AddChapterToBook(int bookId, AddChapterViewModel model)
//{
//    var book = _repository.GetBookById(bookId);
//    if (book != null)
//    {
//        if (book.Chapters == null)
//        {
//            book.Chapters = new List<Chapter>();
//        }
//        int chapterCount = (book != null && book.Chapters != null) ? book.Chapters.Count : 0;
//        var newChapter = new Chapter(chapterCount + 1, model.Title, model.Text, model.Price);
//        book.Chapters.Add(newChapter);
//        var result = _repository.ReplaceBookById(bookId, book);
//        if (result == true)
//        {
//            return new Response<bool>("Correct", result, System.Net.HttpStatusCode.OK);
//        }
//    }
//    return new Response<bool>("Something wrong", false, System.Net.HttpStatusCode.NotFound);
//}

//public Response<bool> AddCommentToBook(int bookId, Comment comment)
//{
//    var book = _repository.GetBookById(bookId);
//    if (book != null)
//    {
//        if (book.Comments == null)
//        {
//            book.Comments = new List<Comment>();
//        }
//        book.Comments.Add(comment);

//        int totalComments = book.Comments.Count;
//        var likedComments = book.Comments.Count(c => c.Liked);
//        double percent = (double)likedComments / totalComments * 100;
//        book.LikedPercent = (int)percent;
//        var result = _repository.ReplaceBookById(bookId, book);
//        if (result == true)
//        {
//            return new Response<bool>("Correct", result, System.Net.HttpStatusCode.OK);
//        }
//    }
//    return new Response<bool>("Something wrong with add comment", false, System.Net.HttpStatusCode.NotFound);

//}