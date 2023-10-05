using System.Runtime.CompilerServices;
using MongoDB.Bson;
using MongoDB.Driver;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Repository.Interfaces;
using static MongoDB.Driver.WriteConcern;

namespace NovelsRanboeTranslates.Repository.Repositories;

public class BookStatisticRepository : BaseRepository<BookStatistic>, IBookStatisticRepository
{
    public BookStatisticRepository(IMongoDbSettings settings) : base(settings, "BookStatistic") { }
    public async Task<bool> Exists(int bookId)
    {
        var filter = Builders<BookStatistic>.Filter.Eq("_id", bookId);
        var result = await _collection.Find(filter).FirstOrDefaultAsync();

        return result != null;
    }

    public async Task<bool> Create(BookStatistic bookStatistic)
    {
        try
        {
            await _collection.InsertOneAsync(bookStatistic);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<BookStatistic> GetOneBookStatistic(int bookId)
    {
        var filter = Builders<BookStatistic>.Filter.Eq("_id", bookId);
        var result = await _collection.Find(filter).FirstOrDefaultAsync();

        return result;
    }

    public async Task<bool> AddToTotalEarnings(int bookId, decimal addedValue)
    {
        try
        {
            var filter = Builders<BookStatistic>.Filter.Eq("_id", bookId);
            var bookStatistic = await _collection.Find(filter).FirstOrDefaultAsync();

            if (bookStatistic != null)
            {
                bookStatistic.TotalEarnings += addedValue;
                bookStatistic.TotalBuyCounter++; ;

                var update = Builders<BookStatistic>.Update
                    .Set("TotalEarnings", bookStatistic.TotalEarnings)
                    .Set("TotalBuyCounter", bookStatistic.TotalBuyCounter);
                var updateResult = await _collection.UpdateOneAsync(filter, update);

                return updateResult.ModifiedCount > 0;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }


    public async Task<bool> UpdateOneChapterStatistic(int bookId, ChaptersStatistic chaptersStatistic)
    {
        try
        {
            var bookFilter = Builders<BookStatistic>.Filter.Eq("_id", bookId);
            var chapterFilter = Builders<BookStatistic>.Filter.Eq("ChaptersStatistic.ChapterId", chaptersStatistic.ChapterId);

            var updateDefinition = Builders<BookStatistic>.Update
                .Set("ChaptersStatistic.$.Earnings", chaptersStatistic.Earnings)
                .Set("ChaptersStatistic.$.BuyCounter", chaptersStatistic.BuyCounter)
                .Set("ChaptersStatistic.$.ReadCounter", chaptersStatistic.ReadCounter);

            var updateResult = await _collection.UpdateOneAsync(bookFilter & chapterFilter, updateDefinition);

            return updateResult.ModifiedCount > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<ChaptersStatistic> GetOneChapterStatistic(int bookId, int chapterId)
    {
        var bookFilter = Builders<BookStatistic>.Filter.Eq("_id", bookId);
        var book = await _collection.Find(bookFilter).FirstOrDefaultAsync();

        return book.ChaptersStatistic.FirstOrDefault(cs => cs.ChapterId == chapterId);
    }

    public async Task<bool> AddOneChapterToBook(int bookId, ChaptersStatistic chaptersStatistic)
    {
        try
        {
            var bookFilter = Builders<BookStatistic>.Filter.Eq("_id", bookId);

            var updateDefinition = Builders<BookStatistic>.Update
                .Push("ChaptersStatistic", chaptersStatistic);

            var updateResult = await _collection.UpdateOneAsync(bookFilter, updateDefinition);

            return updateResult.ModifiedCount > 0;
        }
        catch (Exception e)
        {
            return false;
        }
    }


    public async Task<List<BookStatistic>> GetBookStatisticList()
    {
        var res = await _collection.Find(_ => true).ToListAsync();
        return res;
    }
}