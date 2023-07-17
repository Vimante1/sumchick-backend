using MongoDB.Bson;
using MongoDB.Driver;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Repository.Interfaces;

namespace NovelsRanboeTranslates.Repository.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IMongoCollection<Book> _collection;

        public BookRepository()
        {
            var client = new MongoClient("mongodb://root:example@132.226.192.36:27017");
            var database = client.GetDatabase("Translates");
            _collection = database.GetCollection<Book>("Book");
        }

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
            };
        }

        public Response<List<Book>> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Book> GetBestBooksByGenre(List<string> genres)
        {
            var bestBooks = new List<Book>();
            foreach (var genre in genres)
            {
                var filter = Builders<Book>.Filter.Eq("Genre", genre);
                var sort = Builders<Book>.Sort.Descending("LikedPercent");
                var book = _collection.Find(filter).Sort(sort).Limit(1).FirstOrDefault();
                if (book != null)
                {
                    bestBooks.Add(book);
                }
            }
            return bestBooks;
        }

        public List<Book> GetLatestBooks()
        {
            var sort = Builders<Book>.Sort.Descending("Created");
            var latestBooks = _collection.Find(_ => true).Sort(sort).Limit(15).ToList();
            return latestBooks;

        }

        public Book GetBookById(int bookId)
        {
            try
            {
                var filter = Builders<Book>.Filter.Eq("_id", bookId);
                var book = _collection.Find(filter).FirstOrDefault();
                if (book != null) return book;
                else return null;
            }
            catch
            {
                return null;
            }
        }

        public bool ReplaceBookById(int bookId, Book newBook)
        {
            try
            {
                var filter = Builders<Book>.Filter.Eq("_id", bookId);
                var result = _collection.ReplaceOneAsync(filter, newBook);
                if (result != null) { return true; }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public Response<bool> Delete(Book entity)
        {
            throw new NotImplementedException();
        }
    }
}
