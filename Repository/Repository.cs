using Data;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        protected DbSet<T> DbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }
        public async Task<int> AddAsync(T entity)
        {
            try
            {
                DbSet.Add(entity);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<T>> BulkCreateAsync(IEnumerable<T> entities)
        {
            try
            {
                DbSet.AddRange(entities);
                await _context.SaveChangesAsync();
                return entities;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> BulkDeleteAsync(IEnumerable<T> entities)
        {
            try
            {
                DbSet.AttachRange(entities);
                DbSet.RemoveRange(entities);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<T>> BulkUpdateAsync(IEnumerable<T> entities)
        {
            try
            {
                DbSet.UpdateRange(entities);
                await _context.SaveChangesAsync();
                return entities;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> CreateAsync(T entity)
        {
            DbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
                var res = await _context.SaveChangesAsync();
                return res > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IQueryable<T> GetAll()
        {
            try
            {
                return DbSet.AsNoTracking().AsQueryable();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await DbSet.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T?> GetSingleAsync(int id)
        {
            try
            {
                return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                var dbEntityEntry = _context.Entry(entity);
                dbEntityEntry.State = EntityState.Modified;
                var res = await _context.SaveChangesAsync();
                return res > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
