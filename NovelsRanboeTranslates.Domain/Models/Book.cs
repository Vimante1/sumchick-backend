namespace NovelsRanboeTranslates.Domain.Models
{
    public class Book
    {
        public int _id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Author { get; set; }
        public string OriginalLanguage { get; set; }
        public List<string> Genre { get; set; }
        public int Views { get; set; }
        public DateTime Created { get; set; }
        public int? LikedPercent { get; set; }

        public Book(string title, string description, string author, string originalLanguage, List<string> genre, string imagePath)
        {
            Random random = new();
            _id = random.Next(999999999);
            Title = title;
            Description = description;
            Author = author;
            OriginalLanguage = originalLanguage;
            Genre = genre;
            Created = DateTime.Now;
            Views = 0;
            ImagePath = imagePath;
        }
    }
}
