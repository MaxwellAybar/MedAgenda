using System;

namespace MedAgenda.Application.Dtos.Provider
{
    public class CreateProviderDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
