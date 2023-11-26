using iPractice.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPractice.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        protected ILogger<T> _logger { get; set; }

        public GenericRepository(DbContext dbContext, ILogger<T> logger)
        {
            DbContext = (ApplicationDbContext)dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet = DbContext.Set<T>();
            _logger = logger;
        }

        public async Task<T> FindAsync(params object[] keyValues)
        {
            return await DbContext.Set<T>().FindAsync(keyValues);
        }

        public async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                await DbSet.AddAsync(entity);
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurs during entity creation.");
                throw;
            }

            return entity;
        }

        public async Task<IEnumerable<T>> CreateAsync(IEnumerable<T> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    if (entity == null)
                    {
                        throw new ArgumentNullException(nameof(entities));
                    }
                    await DbSet.AddAsync(entity);
                }

                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurs during entities creation.");
                throw;
            }

            return entities;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            try
            {
                DbContext.Attach(entity);
                DbContext.Entry(entity).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurs during entity update.");
                throw;
            }

            return entity;
        }

        public async Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    if (entity == null)
                    {
                        throw new ArgumentNullException(nameof(entities));
                    }
                    DbContext.Attach(entity);
                    DbContext.Entry(entity).State = EntityState.Modified;
                }

                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurs during entities update.");
                throw;
            }

            return entities;
        }
    }
}
