using System;

namespace Aktywni.Infrastructure.DTO
{
    public class AccountDTO
    {
        public Guid Id {get; set;}
        public string Email {get; set;}
        public string Role {get; set;}
        public string Login {get; set;} 
    }
}