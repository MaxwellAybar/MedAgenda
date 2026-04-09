using System;

namespace MedAgenda.Application.Dtos.Authentication
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}