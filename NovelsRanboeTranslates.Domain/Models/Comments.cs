namespace NovelsRanboeTranslates.Domain.Models
{
    public class Comments
    {
        public int _id { get; set; }
        public List<Comment> Comment { get; set; }
        public Comments(int id)
        {
            _id = id;
            Comment = new List<Comment>();
        }
    }


    public class Comment
    {
        public string AuthorComment { get; set; }
        public string Text { get; set; }
        public bool Liked { get; set; }
        public string Time { get; set; }

        public Comment(string authorComment, string text, bool liked)
        {
            AuthorComment = authorComment;
            Text = text; 
            Liked = liked;
            DateTime currentTime = DateTime.Now;
            string formattedDateTime = currentTime.ToString("dd.MM.yy HH:mm");
            Time = formattedDateTime;
        }
    }


}
