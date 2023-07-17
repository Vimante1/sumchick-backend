using MongoDB.Bson;

namespace NovelsRanboeTranslates.Domain.Models
{
    public class User
    {
        public ObjectId _id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public double Balance { get; set; }
        public List<Purchased> Purchased { get; set; }
        public List<string> FavoritesBookID { get; set; }

        public User()
        {
            Role = "User";
        }
    }
}
