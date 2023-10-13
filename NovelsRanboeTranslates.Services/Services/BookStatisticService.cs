using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Repository.Interfaces;
using NovelsRanboeTranslates.Services.Interfaces;
using ZstdSharp.Unsafe;

namespace NovelsRanboeTranslates.Services.Services;

public class BookStatisticService : IBookStatisticService
{
    private readonly IBookStatisticRepository _repository;

    public BookStatisticService(IBookStatisticRepository bookStatisticRepository)
    {
        _repository = bookStatisticRepository;
    }

    public async Task<bool> AddPurchasedChapter(int bookId, int chapterId, decimal price)
    {
        try
        {
            if (await _repository.Exists(bookId))
            {
                var chapter = await _repository.GetOneChapterStatistic(bookId, chapterId);
                if (chapter != null)
                {
                    await _repository.AddToTotalEarnings(bookId, price);
                    chapter.Earnings += price;
                    chapter.BuyCounter += 1;
                    await _repository.UpdateOneChapterStatistic(bookId, chapter);
                    return true;
                }
                else
                {
                    await _repository.AddToTotalEarnings(bookId, price);
                    await _repository.AddOneChapterToBook(bookId, new ChaptersStatistic(chapterId, price, 1, 0));
                    return true;
                }
            }
            else
            {
                var book = new BookStatistic(bookId);
                book.TotalBuyCounter++;
                book.TotalEarnings = price;
                book.ChaptersStatistic.Add(new ChaptersStatistic(chapterId, price, 1, 0));
                await _repository.Create(book);
                return true;
            }
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public Task<List<BookStatistic>> GetStatisticList()
    {
        return _repository.GetBookStatisticList();
    }

    public async void AddOneReadToChapterCounter(int bookId, int chapterId)
    {
        if (await _repository.Exists(bookId))
        {
            var chapter = await _repository.GetOneChapterStatistic(bookId, chapterId);
            if (chapter != null)
            {
                chapter.ReadCounter++;
                await _repository.UpdateOneChapterStatistic(bookId, chapter);
            }
            else
            {
                var newChapter = new ChaptersStatistic(chapterId, 0, 0, 1)
                { ChapterId = chapterId, BuyCounter = 0, Earnings = 0, ReadCounter = 1 };
                await _repository.AddOneChapterToBook(bookId, newChapter);
            }
        }
        else
        {
            var newBookStatistic = new BookStatistic(bookId);
            var newChapter = new ChaptersStatistic(chapterId, 0, 0, 1);
            newBookStatistic.ChaptersStatistic.Add(newChapter);
            await _repository.Create(newBookStatistic);
        }
    }
}