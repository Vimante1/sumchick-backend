using NovelsRanboeTranslates.Domain.DTOs;
using NovelsRanboeTranslates.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelsRanboeTranslates.Repository.Interfaces
{
    public interface IChapterRepository : IBaseRepository<Chapters>
    {
        public Task<bool> CreateChaptersAsync(Chapters chapters);
        public Task<Chapters> GetChaptersAsync(int id);
        public Task<ChaptersDTO> GetChaptersDTOAsync(int id);
        public Task<bool> UpdateChaptersAsync(Chapters chapters);
    }
}
