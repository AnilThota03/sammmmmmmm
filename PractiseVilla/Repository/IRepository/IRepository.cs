using System.Linq.Expressions;

namespace PractiseVilla.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {

        Task Create(T entity);

        Task Delete(T entity);

        Task<List<T>> GetAll(Expression<Func<T, bool>> filter=null);

        Task<T> GetOne(Expression<Func<T, bool>> filter=null);

        Task Save();
    }
}
