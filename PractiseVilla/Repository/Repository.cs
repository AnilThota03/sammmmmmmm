using Microsoft.EntityFrameworkCore;
using PractiseVilla.Data;
using PractiseVilla.Repository.IRepository;
using System.Linq.Expressions;

namespace PractiseVilla.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbset;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbset = _db.Set<T>();
        }

        public async Task Create(T entity)
        {
            await dbset.AddAsync(entity);
            await Save();

        }

        public async Task Delete(T entity)
        {
            dbset.Remove(entity);
            await Save();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetOne(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }


        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        
    }
}
