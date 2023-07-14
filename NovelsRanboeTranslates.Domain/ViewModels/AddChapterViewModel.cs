using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelsRanboeTranslates.Domain.ViewModels
{
    public class AddChapterViewModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public double Price { get; set; }

    }
}
