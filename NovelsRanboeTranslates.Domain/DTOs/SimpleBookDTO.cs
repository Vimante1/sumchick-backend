using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelsRanboeTranslates.Domain.DTOs
{
    public class SimpleBookDTO
    {
        public int _id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Author { get; set; }
        public string OriginalLanguage { get; set; }
        public string Genre { get; set; }
        public int Views { get; set; }
        public int? LikedPercent { get; set; }
    }
}
