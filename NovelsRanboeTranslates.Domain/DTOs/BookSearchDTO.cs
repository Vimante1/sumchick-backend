namespace NovelsRanboeTranslates.Domain.DTOs
{
    public class BookSearchDTO
    {
        public int _id { get; set; }

        public string Title { get; set; }

        public BookSearchDTO(int bookID, string title)
        {
            Title = title;
            _id = bookID;
        }
    }
}
