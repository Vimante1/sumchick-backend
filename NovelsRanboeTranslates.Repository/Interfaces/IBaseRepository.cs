using NovelsRanboeTranslates.Domain.Models;

namespace NovelsRanboeTranslates.Repository.Interfaces
{
    public interface IBaseRepository<T>
    {
        Response<bool> Create(T entity);
        Response<List<T>> GetAll();
        Response<bool> Delete(T entity);
    }
}
