namespace NovelsRanboeTranslates.Domain.Models
{
    public class Comment
    {
        public string AuthorComment { get; set; }
        public string Text { get; set; }
        public bool Liked { get; set; }

        public Comment(string authorComment, string text, bool liked)
        {
            AuthorComment = authorComment;
            Text = text;
            Liked = liked;
        }
    }


}
