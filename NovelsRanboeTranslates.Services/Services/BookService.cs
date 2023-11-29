using NovelsRanboeTranslates.Domain.DTOs;
using NovelsRanboeTranslates.Domain.Lists;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;
using NovelsRanboeTranslates.Repository.Interfaces;
using NovelsRanboeTranslates.Services.Interfraces;
using Microsoft.Extensions.Caching.Memory;
using static System.Reflection.Metadata.BlobBuilder;

namespace NovelsRanboeTranslates.Services.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IMemoryCache _memoryCache;

        public BookService(IBookRepository repository, IMemoryCache memoryCache)
        {
            _repository = repository;
            _memoryCache = memoryCache;
        }

        public Response<bool> CreateNewBook(CreateNewBookViewModel book, string imagePath)
        {
            var newBook = new Book(book.Title, book.Description, book.Author, book.OriginalLanguage, book.Genre, imagePath);
            return _repository.Create(newBook);
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _repository.GetAllBooks();
        }

        public async Task<bool> UpdateBook(UpdateBookViewModel updateBook)
        {
            return await _repository.UpdateBook(updateBook);
        }

        public async Task<List<Book>> GetBestBooksByGenre()
        {
            
            try
            {
                if (!_memoryCache.TryGetValue("123123", out List<Book> result))
                {
                    var genres = new Genres().GetList();
                    var books = await _repository.GetBestBooksByGenreAsync(genres);

                    _memoryCache.Set("123123", books, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2)
                    });
                    return books;
                }
                return result;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                return new List<Book> { };
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