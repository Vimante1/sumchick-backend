namespace NovelsRanboeTranslates.Domain.Models;

public class BookStatistic
{
    public int _id { get; set; }
    public decimal TotalEarnings { get; set; }
    public int TotalBuyCounter { get; set; }
    public List<ChaptersStatistic> ChaptersStatistic { get; set; }

    public BookStatistic(int bookId)
    {
        _id = bookId;
        TotalEarnings = 0;
        TotalBuyCounter = 0;
        ChaptersStatistic = new List<ChaptersStatistic>();
    }
}

public class ChaptersStatistic
{
    public int ChapterId { get; set; }
    public decimal Earnings { get; set; }
    public int BuyCounter { get; set; }
    public int ReadCounter { get; set; }

    public ChaptersStatistic(int chapterId, decimal earnings, int buyCounter, int readCounter)
    {
        ChapterId = chapterId;
        Earnings = earnings;
        BuyCounter = buyCounter;
        ReadCounter = readCounter;
    }
}