using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Abstractions
{

    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<List<T>> QueryAsync(Expression<Func<T, bool>> condition, bool tracking = false);
        Task<List<T>> Pagination(Paging paging,bool tracking = false);

        Task<T?> GetByIdAsync(Guid id, bool tracking = false);

        Task Update(T entity);

        Task DeleteAsync(Guid id);

        Task SaveChangesAsync();
        Task AddAsync(T entity);
        Task<List<T>> GetAllAsync();
    }
}
