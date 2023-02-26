using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetSingleAsync(int id);
        Task<int> AddAsync(T entity);
        Task<T> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<IEnumerable<T>> BulkCreateAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> BulkUpdateAsync(IEnumerable<T> entities);
        Task<int> BulkDeleteAsync(IEnumerable<T> entities);
    }
}
