using Airline.Core.Entities.Common;
using Airline.DAL.Context;
using Airline.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airline.DAL.Repositories.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly AppDbContext _dbcontext;
        private DbSet<TEntity> _table;
        public Repository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            _table = _dbcontext.Set<TEntity>();
        }
        public DbSet<TEntity> Table => _dbcontext.Set<TEntity>();

        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression = null,
            Expression<Func<TEntity, object>>? OrderByExpression = null,
            bool isDescending = false
            , params string[] includes)
        {
            IQueryable<TEntity> query = Table.Where(b => b.IsDeleted == false);
            if (expression is not null)
            {
                query = query.Where(expression);
            }
            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }

            if (OrderByExpression != null)
            {
                query = isDescending ? query.OrderByDescending(OrderByExpression) : query.OrderBy(OrderByExpression);
            }


            return query;
        }

        public async Task<TEntity> GetByIdAsync(int id, params string[] includes)
        {
            IQueryable<TEntity> query = Table.Where(c => c.Id == id && c.IsDeleted == false);
            if (includes is not null && query.Count() > 0)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            var entity = await query.FirstOrDefaultAsync(c => c.Id == id);
            return entity;
        }
        public async Task Create(TEntity entity)
        {
            await _table.AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            _table.Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _table.Remove(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _dbcontext.SaveChangesAsync();
        }
        public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> expression)
        {
            return _dbcontext.Set<TEntity>().Where(expression);
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, params string[] Includes)
        {
            var query = _dbcontext.Set<TEntity>().Where(expression);

            if (Includes != null)
            {
                foreach (string include in Includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync(); ;
        }
    }
}
