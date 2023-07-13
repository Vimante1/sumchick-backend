using MongoDB.Bson;

namespace NovelsRanboeTranslates.Domain.Models
{
    public class User
    {
        public ObjectId _id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; }
        public double Balance { get; set; }
        public List<Purchased> Purchaseds { get; set; }
        public List<string> FavoritesBookID { get; set; }

        public User()
        {
            Role = "User";
        }
    }

    public class Purchased
    {
        public string BookID { get; set; }
        public HashSet<string> ChapterID { get; set; }
    }
}
