using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPractice.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> FindAsync(params object[] keyValues);
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> CreateAsync(IEnumerable<T> entities);
        Task<T> UpdateAsync(T entity);
        Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities);
    }
}
