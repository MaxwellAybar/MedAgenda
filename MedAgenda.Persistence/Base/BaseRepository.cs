using Microsoft.EntityFrameworkCore;
using MedAgenda.Persistence.Context;

namespace MedAgenda.Persistence.Base
{
    public class BaseRepository<T> where T : class
    {
        private readonly MedAgendaContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(MedAgendaContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los datos", ex);
            }
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el registro con id {id}", ex);
            }
        }

        public virtual async Task AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar en la base de datos", ex);
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el registro", ex);
            }
        }

        public virtual async Task DeleteAsync(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el registro", ex);
            }
        }
    }
}