using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Infrustructure.Repositories
{
    public class UserRepository : IUserREpository
    {
        public AppDbContext _context { get; set; }
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User entity)
        {
           await _context.Users.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await GetByIdAsync(id);
            if (user != null) {
             _context.Users.Remove(user);
            }
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id, bool tracking = false)
        {
            var query = _context.Users.AsQueryable();
            if (!tracking) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<List<User>> Pagination(Expression<Func<User, bool>> predicate, Paging paging, bool tracking = false)
        {
            var query = _context.Users.AsQueryable();

            if (!tracking) query = query.AsNoTracking();
            return await query
                .Where(predicate)
                .Skip(paging.Skip)
                .Take(paging.PageSize)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<User>> QueryAsync(Expression<Func<User, bool>> condition, bool tracking = false)
        {
            var query = _context.Users.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            var entities = await query.Where(condition).ToListAsync();
            return entities;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task Update(User entity)
        {
            _context.Users.Update(entity);
            return Task.CompletedTask;
        }
    }
}
