using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAgenda.Domain.Base;


namespace MedAgenda.Domain.Entities
{
    public class Users
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; } 

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}