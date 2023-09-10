using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelsRanboeTranslates.Domain.ViewModels
{
    public class AdvancedSearchViewModel
    {
        public string OriginalLanguage { get; set; }
        public int SortType { get; set; }
        public string[] Genres { get; set; }
        public int SkipCounter { get; set; }
    }
}
