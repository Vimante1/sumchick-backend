using MongoDB.Bson;
using MongoDB.Driver;
using NovelsRanboeTranslates.Domain.DTOs;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Repository.Interfaces;
using System.Linq.Expressions;
using NovelsRanboeTranslates.Domain.ViewModels;
using static System.Reflection.Metadata.BlobBuilder;
using System.Net;

namespace NovelsRanboeTranslates.Repository.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(IMongoDbSettings settings) : base(settings, "Book"){}

        public Response<bool> Create(Book entity)
        {
            try
            {
                _collection.InsertOne(entity);
                return new Response<bool>("Correct", true, System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new Response<bool>(ex.Message, false, System.Net.HttpStatusCode.BadRequest);
            }

            ;
        }

        public async Task<bool> UpdateBook(UpdateBookViewModel updateBook)
        {
            var filter = Builders<Book>.Filter.Eq("_id", updateBook._id);

            var update = Builders<Book>.Update
                .Set(b => b.Title, updateBook.Title)
                .Set(b => b.Description, updateBook.Description)
                .Set(b => b.Author, updateBook.Author)
                .Set(b => b.OriginalLanguage, updateBook.OriginalLanguage)
                .Set(b => b.Genre, updateBook.Genre);

            var result = await _collection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        public async Task<List<Book>> GetBestBooksByGenreAsync(List<string> genres)
        {
            var filter = Builders<Book>.Filter.In("Genre", genres);
            var sort = Builders<Book>.Sort.Descending("LikedPercent");

            var bestBooks = await _collection.Find(filter)
                .Sort(sort)
                .Limit(20)
                .ToListAsync();

            return bestBooks;
        }

        public async Task<List<Book>> GetLatestBooksAsync()
        {
            var sort = Builders<Book>.Sort.Descending("Created");
            var latestBooks = await _collection.Find(_ => true)
                .Sort(sort)
                .Limit(20)
                .ToListAsync();
            return latestBooks;
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            try
            {
                var filter = Builders<Book>.Filter.Eq("_id", bookId);
                var book = await _collection.Find(filter).FirstOrDefaultAsync();
                return book;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateLikedPercentBookAsync(Book book, int likedPercent)
        {
            try
            {
                var filter = Builders<Book>.Filter.Eq(b => b._id, book._id);
                var update = Builders<Book>.Update.Set(u => u.LikedPercent, likedPercent);
                await _collection.UpdateOneAsync(filter, update);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ReplaceBookById(int bookId, Book newBook)
        {
            try
            {
                var filter = Builders<Book>.Filter.Eq("_id", bookId);
                var result = _collection.ReplaceOneAsync(filter, newBook);
                if (result != null)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<BookSearchDTO>> SearchBookByName(string name)
        {
            var filter = Builders<Book>.Filter.Regex("Title", new BsonRegularExpression(name, "i"));
            var book = await _collection.Find(filter).ToListAsync();
            var result = book.Select(book => new BookSearchDTO(book._id, book.Title)).ToList();
            return result;
        }

        public async void AddViewToBookById(int bookId)
        {
            var filter = Builders<Book>.Filter.Eq("_id", bookId);
            var update = Builders<Book>.Update.Inc("Views", 1);
            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public Response<bool> Delete(Book entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Book>> AdvancedSearch(string originalLanguage, int sortType, string[] genres, int skipCounter)
        {
            var filterBuilder = Builders<Book>.Filter;
            var filter = filterBuilder.Empty;
            filter &= genres.Length != 0 ? filterBuilder.All("Genre", genres) : filter;
            filter &= originalLanguage != "Any" ? filterBuilder.Eq("OriginalLanguage", originalLanguage) : filter;

            var sortDefinition = sortType switch
            {
                0 => Builders<Book>.Sort.Ascending(p => p.Title),           // 0 = Sorted by name
                1 => Builders<Book>.Sort.Descending(p => p.Created),        // 1 = New books
                2 => Builders<Book>.Sort.Descending(p => p.LikedPercent),   // 2 = Best books by liked percent
                _ => null
            }; 

            var result = await _collection.Find(filter)
                .Sort(sortDefinition)
                .Skip(skipCounter)
                .Limit(20)
                .ToListAsync();

            return result;
        }

    }
}
