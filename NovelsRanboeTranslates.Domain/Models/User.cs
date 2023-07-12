using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelsRanboeTranslates.Domain.Models
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; } = "User";
        public double Balance { get; set; }
        public List<Purchased> Purchaseds { get; set; }
        public List<string> FavoritesBookID { get; set; }
    }

    public class Purchased
    {
        public string BookID { get; set; }
        public HashSet<string> ChapterID { get; set; }
    }
}
