using NovelsRanboeTranslates.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelsRanboeTranslates.Repository.Interfaces
{
    public interface IBaseRepository<T>
    {
        Response<bool> Create(T entity);
        Response<List<T>> GetAll();
        Response<bool> Delete(T entity);
    }
}
