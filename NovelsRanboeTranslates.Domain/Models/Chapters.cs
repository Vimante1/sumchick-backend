namespace NovelsRanboeTranslates.Domain.Models
{
    public class Chapters
    {
        public int _id { get; set; }
        public List<Chapter> Chapter { get; set; }
        public Chapters(int id)
        {
            _id = id;
            Chapter = new List<Chapter>();
        }
    }

    public class Chapter
    {
        public int ChapterId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public decimal Price { get; set; }
        public bool HasPrice { get; set; }

        public Chapter(int chapterId, string title, string text, decimal price)
        {
            ChapterId = chapterId;
            Title = title;
            Text = text;
            Price = price;
            HasPrice = Price > 0;
        }
    }
}
