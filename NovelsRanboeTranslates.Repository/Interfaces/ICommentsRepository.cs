using NovelsRanboeTranslates.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelsRanboeTranslates.Repository.Interfaces
{
    public interface ICommentsRepository : IBaseRepository<Comments>
    {
        public Task<bool> CreateCommentsAsync(Comments comments);
        public Task<bool> UpdateCommentsAsync(Comments comments);
        public Task<Comments> GetCommentsAsync(int id);

    }
}
