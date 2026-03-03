using Microsoft.EntityFrameworkCore;
using MedAgenda.Persistence.Context;
using System.Linq.Expressions;

namespace MedAgenda.Persistence.Base
{
    public class BaseRepository<T> where T : class
    {
        protected readonly MedAgendaContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(MedAgendaContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}