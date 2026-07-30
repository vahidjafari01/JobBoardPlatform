using Dapper;
using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Users;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public UserRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<UserDto>> GetJobSeekers(int take,int skip)
        {
            using var sqlconnection = new SqlConnection(_configuration["ConnectionStrings:Sql"]);
            var query = @"select u.Id As UserId,u.UserName,u.Email from AspNetUsers as u join AspNetUserRoles as r on u.Id=r.UserId 
                        where r.RoleId ='3E9F489C-E97F-40DC-85C3-76CE5378303D'  order by u.CreatedAt
                        offset @Skip rows fetch next @Take rows only;";
            var result = await sqlconnection.QueryAsync<UserDto>(query, new {Skip=skip,Take = take });
            return result.ToList();
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

        public async Task<int> GetJobSeekerCount()
        {
            var query = @"select COUNT(*) from AspNetUsers as u join AspNetUserRoles as r on u.Id = r.UserId where r.RoleId = '3E9F489C-E97F-40DC-85C3-76CE5378303D';";

            using var connection = new SqlConnection(_configuration["ConnectionStrings:Sql"]);
            return await connection.ExecuteScalarAsync<int>(query);
        }
        public async Task<int> GetEmployerCount()
        {
            var query = @"select COUNT(*) from AspNetUsers as u join AspNetUserRoles as r on u.Id = r.UserId where r.RoleId = 'E5E54FE9-0F12-4B07-9243-3471EBE491BC';";
            using var connection = new SqlConnection(_configuration["ConnectionStrings:Sql"]);
            return await connection.ExecuteScalarAsync<int>(query);
        }
    }
}
