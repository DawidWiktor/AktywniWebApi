using System;

namespace Aktywni.Infrastructure.DTO
{
    public class AccountDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
    }
}