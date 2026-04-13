using MedAgenda.Application.Dtos.User;
using MedAgenda.Application.Interfaces;
using MedAgenda.Persistence.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedAgenda.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository repository, ILogger<UserService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            try
            {
                var users = await _repository.GetAllAsync();
                return users.Select(u => new UserDto
                {
                    Id = u.Id,
                    UserName = u.Username,
                    Email = u.Email,
                    Role = u.Role
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error GetAllUsersAsync");
                return new List<UserDto>();
            }
        }
    }
}
