using MongoDB.Bson;
using MongoDB.Driver.Core.Operations;
using NovelsRanboeTranslates.Domain.Lists;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;
using NovelsRanboeTranslates.Repository.Interfaces;
using NovelsRanboeTranslates.Services.Interfraces;
using System.Net.WebSockets;

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
        public Response<List<List<Book>>> GetBooksCompilation()
        {
            List<List<Book>> compilation = new List<List<Book>>();
            var genres = new Genres().GetList();
            compilation.Add(_repository.GetBestBooksByGenre(genres));
            compilation.Add(_repository.GetLatestBooks());
            return new Response<List<List<Book>>>("1.BestBooksByGenre 2.LatestBooks", compilation, System.Net.HttpStatusCode.OK);
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
            return new Response<bool>("Something wrong", result, System.Net.HttpStatusCode.NotFound);

        }
    }

}
