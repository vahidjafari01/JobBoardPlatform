using JobBoardPlatform.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Users
{
    public interface IUserREpository
    {
        Task<List<User>> QueryAsync(Expression<Func<User, bool>> condition, bool tracking = false);
        Task<List<User>> Pagination(Expression<Func<User, bool>> predicate, Paging paging, bool tracking = false);

        Task<User?> GetByIdAsync(Guid id, bool tracking = false);

        Task Update(User entity);

        Task DeleteAsync(Guid id);

        Task SaveChangesAsync();
        Task AddAsync(User entity);
        Task<List<User>> GetAllAsync();
    }
}
