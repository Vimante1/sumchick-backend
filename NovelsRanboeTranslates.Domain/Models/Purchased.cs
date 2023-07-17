namespace NovelsRanboeTranslates.Domain.Models
{
    public class Purchased
    {
        public int BookID { get; set; }
        public HashSet<int> ChapterID { get; set; }

        public Purchased(int bookID, int chapterID)
        {
            BookID = bookID;
            ChapterID = new HashSet<int> { };
            ChapterID.Add(chapterID);
        }
    }
}
