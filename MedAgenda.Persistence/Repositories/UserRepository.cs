using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedAgenda.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MedAgendaContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(MedAgendaContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Users>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task AddAsync(Users user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Usuario agregado con éxito");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar usuario");
                throw;
            }
        }

        public async Task UpdateAsync(Users user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Usuario actualizado con éxito");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar usuario");
                throw;
            }
        }

        public async Task DeleteAsync(Users user)
        {
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Usuario eliminado con éxito");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar usuario");
                throw;
            }
        }
    }
}