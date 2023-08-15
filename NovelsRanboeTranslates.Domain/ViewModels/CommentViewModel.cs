using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelsRanboeTranslates.Domain.ViewModels
{
    public class CommentViewModel
    {
        public int BookId { get; set; }
        public string Text { get; set; }
        public bool Liked { get; set; }
    }
}
