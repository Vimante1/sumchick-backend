using MongoDB.Bson;
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
            else return new Response<Book>("BookNotFound", null, System.Net.HttpStatusCode.NotFound);
        }
    }

}
