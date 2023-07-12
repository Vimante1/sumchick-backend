namespace NovelsRanboeTranslates.Domain.Models
{
    public class Chapter
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public double Price { get; set; }
        public bool HasPrice { get; set; }

        public Chapter( int id, string title, string text, double price)
        {
            ID = id; 
            Title = title;
            Text = text;
            Price = price;
            HasPrice = Price > 0;
        }
    }


}
