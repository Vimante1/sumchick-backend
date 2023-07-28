using NovelsRanboeTranslates.Domain.DTOs;
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
        public Response<List<SimpleBookDTO>> GetBestBooksByGenre()
        {
            try
            {
                var genres = new Genres().GetList();
                var books = _repository.GetBestBooksByGenre(genres);
                return new Response<List<SimpleBookDTO>>("Correct", books, System.Net.HttpStatusCode.OK);
            }
            catch
            {
                return new Response<List<SimpleBookDTO>>("Something wrong with get from DB", null, System.Net.HttpStatusCode.BadRequest);
            }
        }
        public Response<List<SimpleBookDTO>> GetLatestBooks()
        {
            try
            {
                var books = _repository.GetLatestBooks();
                return new Response<List<SimpleBookDTO>>("Correct", books, System.Net.HttpStatusCode.OK);
            }
            catch
            {
                return new Response<List<SimpleBookDTO>>("Something wrong with get from DB", null, System.Net.HttpStatusCode.BadRequest);
            }
        }

        public Response<Book> GetBookById(int bookId)
        {
            var book = _repository.GetBookById(bookId);
            if (book != null)
            {
                return new Response<Book>("Correct", book, System.Net.HttpStatusCode.OK);
            }
            return new Response<Book>("BookNotFound", null, System.Net.HttpStatusCode.NotFound);
        }

        public Response<bool> AddChapterToBook(int bookId, AddChapterViewModel model)
        {
            var book = _repository.GetBookById(bookId);
            if (book != null)
            {
                if (book.Chapters == null)
                {
                    book.Chapters = new List<Chapter>();
                }
                int chapterCount = (book != null && book.Chapters != null) ? book.Chapters.Count : 0;
                var newChapter = new Chapter(chapterCount + 1, model.Title, model.Text, model.Price);
                book.Chapters.Add(newChapter);
                var result = _repository.ReplaceBookById(bookId, book);
                if (result == true)
                {
                    return new Response<bool>("Correct", result, System.Net.HttpStatusCode.OK);
                }
            }
            return new Response<bool>("Something wrong", false, System.Net.HttpStatusCode.NotFound);
        }

        public Response<bool> AddCommentToBook(int bookId, Comment comment)
        {
            var book = _repository.GetBookById(bookId);
            if (book != null)
            {
                if (book.Comments == null)
                {
                    book.Comments = new List<Comment>();
                }
                book.Comments.Add(comment);

                int totalComments = book.Comments.Count;
                var likedComments = book.Comments.Count(c => c.Liked);
                double percent = (double)likedComments / totalComments * 100;
                book.LikedPercent = (int)percent;
                var result = _repository.ReplaceBookById(bookId, book);
                if (result == true)
                {
                    return new Response<bool>("Correct", result, System.Net.HttpStatusCode.OK);
                }
            }
            return new Response<bool>("Something wrong with add comment", false, System.Net.HttpStatusCode.NotFound);

        }
    }

}
