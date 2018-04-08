using System;

namespace Aktywni.Core.Domain
{
    public class User : Entity
    {
        public string Login {get; protected set;}
        public string Name {get; protected set;}
        public string Surname {get; protected set;}
        public string Role {get; protected set;}
        public string Email {get; protected set;}
        public string Password {get; protected set;}
        public string Salt {get; protected set;}
        public bool IsActive {get; protected set;}
        public string City {get; protected set;}
        public string Describe {get; protected set;}
        public int Rating {get; protected set;}
        public int NumOfRating {get; protected set;}
        public DateTime CreatedAt {get; protected set;}
        public DateTime DateLastActive {get; protected set;}

        protected User()
        {
        }

        public User(Guid id, string login, string role, string email, string password, string salt)
        {
            Id = id;
            Login = login;
            Role = role;
            Email = email;
            Password = password;
            Salt = salt;
            CreatedAt = DateTime.UtcNow;
            Rating = 0;
            NumOfRating = 0;
        }

    }
}