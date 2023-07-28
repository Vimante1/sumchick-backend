using MongoDB.Bson;
using NovelsRanboeTranslates.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
