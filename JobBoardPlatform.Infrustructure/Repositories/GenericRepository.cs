using JobBoardPlatform.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Infrustructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id, true);
            if (entity is not null)
            {
                _context.Set<T>().Remove(entity);
            }
        }

        public async Task<T?> GetByIdAsync(Guid id, bool tracking = false)
        {
            var query = _context.Set<T>().AsQueryable();
            if (!tracking) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<T>> QueryAsync(Expression<Func<T, bool>> condition, bool tracking = false)
        {
            var query = _context.Set<T>().AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            var entities = await query.Where(condition).ToListAsync();
            return entities;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task Update(T entity)
        {
            _context.Update(entity);
            return Task.CompletedTask;
        }

        public async Task<List<T>> Pagination(Paging paging, bool tracking = false)
        {
            var query = _context.Set<T>().AsQueryable();

            if (!tracking) query = query.AsNoTracking();
            return await query
                .Skip(paging.Skip)
                .Take(paging.PageSize)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }



       
    }
}
