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
            var newBook = new Book(book.Title, book.Description, book.Author, book.OriginalLanguage, book.Genre, imagePath);
            return _repository.Create(newBook);
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

            return book != null ? new Response<Book>("Correct", book, System.Net.HttpStatusCode.OK) : new Response<Book>("BookNotFound", null, System.Net.HttpStatusCode.NotFound);
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

        public async Task<List<BookSearchDTO>> SearchBookByName(string name)
        {
            return await _repository.SearchBookByName(name);
        }

        public void AddViewToBookById(int bookId)
        {
             _repository.AddViewToBookById(bookId);
        }

        public async Task<List<Book>> AdvancedSearch(string originalLanguage, int sortType, string[] genres, int skipCounter)
        {
            try
            {
                var result = await _repository.AdvancedSearch(originalLanguage, sortType, genres, skipCounter);
                return result;
            }
            catch (Exception e)
            {
                return new List<Book>();
            }

        }
    }
    
}