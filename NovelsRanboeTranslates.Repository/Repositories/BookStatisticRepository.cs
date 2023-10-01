using System.Runtime.CompilerServices;
using MongoDB.Bson;
using MongoDB.Driver;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Repository.Interfaces;
using static MongoDB.Driver.WriteConcern;

namespace NovelsRanboeTranslates.Repository.Repositories;

public class BookStatisticRepository : BaseRepository<BookStatistic>, IBookStatisticRepository 
{
    public BookStatisticRepository(IMongoDbSettings settings) : base(settings, "BookStatistic") {}
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

    public async Task<bool> AddToTotalEarnings(int bookId, int addedValue)
    {
        try
        {
            var filter = Builders<BookStatistic>.Filter.Eq("_id", bookId);
            var update = Builders<BookStatistic>.Update.Inc("TotalEarnings", addedValue);
            await _collection.UpdateOneAsync(filter, update);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> UpdateOneChapterStatistic(int bookId, ChaptersStatistic chaptersStatistic)
    {
        try
        {
            var bookFilter = Builders<BookStatistic>.Filter.Eq("_id", bookId);
            var book = await _collection.Find(bookFilter).FirstOrDefaultAsync();

            if (book == null)
            {
                return false;
            }

            var chapterToUpdate = book.ChaptersStatistic.FirstOrDefault(cs => cs.ChapterId == chaptersStatistic.ChapterId);

            if (chapterToUpdate == null)
            {
                return false;
            }

            chapterToUpdate.Earnings = chaptersStatistic.Earnings;
            chapterToUpdate.BuyCounter = chaptersStatistic.BuyCounter;
            chapterToUpdate.ReadCounter = chaptersStatistic.ReadCounter;

            var updateResult = await _collection.ReplaceOneAsync(bookFilter, book);

            return updateResult.ModifiedCount > 0;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<ChaptersStatistic> GetOneChapterStatistic(int bookId, int chapterId)
    {
        var bookFilter = Builders<BookStatistic>.Filter.Eq("_id", bookId);
        var book = await _collection.Find(bookFilter).FirstOrDefaultAsync();

        if (book == null)
        {
            return null;
        }

        return book.ChaptersStatistic.FirstOrDefault(cs => cs.ChapterId == chapterId);
    }

    public async Task<bool> AddOneChapterToBook(int bookId, ChaptersStatistic chaptersStatistic)
    {
        try
        {
            var bookFilter = Builders<BookStatistic>.Filter.Eq("_id", bookId);
            var book = await _collection.Find(bookFilter).FirstOrDefaultAsync();

            if (book == null)
            {
                return false;
            }

            book.ChaptersStatistic.Add(chaptersStatistic);
            var updateResult = await _collection.ReplaceOneAsync(bookFilter, book);
            return updateResult.ModifiedCount > 0;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}