using MongoDB.Bson;
using NovelsRanboeTranslates.Domain.Models;

namespace NovelsRanboeTranslates.Domain.DTOs
{
    public class UserDTO
    {
        public ObjectId _id { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
        public double Balance { get; set; }
        public List<Purchased> Purchased { get; set; }
        public List<string> FavoritesBookID { get; set; }
    }
}
