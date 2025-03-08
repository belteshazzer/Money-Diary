using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MoneyDiary.Common;
using MoneyDiary.Data;

namespace MoneyDiary.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, ISoftDeletable
    {
        private readonly MoneyDiaryDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(MoneyDiaryDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                var result = await _dbSet.ToListAsync();
                var entities = result.Where(e => e.IsDeleted == false);
                return entities;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving {typeof(T).Name} entities: {ex.Message}", ex);
            }
        }

        public async Task<T> GetByIdAsync(object id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id) ?? throw new KeyNotFoundException($"{typeof(T).Name} with the specified id not found");
                if (entity.IsDeleted == true)
                {
                    throw new InvalidOperationException($"{typeof(T).Name} with the specified id already deleted");
                }
                return entity;
            }
            catch (KeyNotFoundException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving {typeof(T).Name} with id {id}: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression)
        {
            try
            {
                return await _dbSet.Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving {typeof(T).Name} entities by condition: {ex.Message}", ex);
            }
        }

        public async Task InsertAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException($"Error inserting {typeof(T).Name}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting {typeof(T).Name}: {ex.Message}", ex);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException($"Concurrency error updating {typeof(T).Name}: {ex.Message}", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException($"Error updating {typeof(T).Name}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating {typeof(T).Name}: {ex.Message}", ex);
            }
        }

        public async Task DeleteAsync(object id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity.IsDeleted == true)
                {
                    throw new InvalidOperationException($"{typeof(T).Name} with the specified id is already deleted");
                }
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException($"Error deleting {typeof(T).Name}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting {typeof(T).Name}: {ex.Message}", ex);
            }
        }

    }
}