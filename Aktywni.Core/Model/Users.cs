using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CryptoHelper;

namespace Aktywni.Core.Model
{
    public partial class Users
    {
        private static readonly Regex NameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");

        public Users()
        {
            Abonaments = new HashSet<Abonaments>();
            EventsAdminNavigation = new HashSet<Events>();
            EventsWhoCreated = new HashSet<Events>();
            FriendsFriendFromNavigation = new HashSet<Friends>();
            FriendsFriendToNavigation = new HashSet<Friends>();
            MessageEventUserFrom = new HashSet<MessageEvent>();
            MessageEventUserTo = new HashSet<MessageEvent>();
            MessageUserUser = new HashSet<MessageUser>();
            MessageUserUserFrom = new HashSet<MessageUser>();
            ObjectComments = new HashSet<ObjectComments>();
            Objects = new HashSet<Objects>();
            UsersEvents = new HashSet<UsersEvents>();
            UserCommentsUserIdRatedNavigation = new HashSet<UserComments>();
            UserCommentsUserIdWhoCommentNavigation = new HashSet<UserComments>();
        }

        public Users(string login, string email, string password)
        {
            Abonaments = new HashSet<Abonaments>();
            EventsAdminNavigation = new HashSet<Events>();
            EventsWhoCreated = new HashSet<Events>();
            FriendsFriendFromNavigation = new HashSet<Friends>();
            FriendsFriendToNavigation = new HashSet<Friends>();
            MessageEventUserFrom = new HashSet<MessageEvent>();
            MessageEventUserTo = new HashSet<MessageEvent>();
            MessageUserUser = new HashSet<MessageUser>();
            MessageUserUserFrom = new HashSet<MessageUser>();
            ObjectComments = new HashSet<ObjectComments>();
            Objects = new HashSet<Objects>();
            UsersEvents = new HashSet<UsersEvents>();
            UserCommentsUserIdRatedNavigation = new HashSet<UserComments>();
            UserCommentsUserIdWhoCommentNavigation = new HashSet<UserComments>();
            SetLogin(login);
            SetEmail(email);
            SetPassword(password);
            SetRoleUser();
            //TODO: zmien na true
            IsActive = true;


        }
        public int UserId { get; protected set; }
        public string Login { get; protected set; }
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public bool? IsActive { get; protected set; }
        public DateTime? DateLastActive { get; protected set; }
        public string Password { get; protected set; }
        public string Email { get; protected set; }
        public string City { get; protected set; }
        public string Role { get; protected set; }
        public string Description { get; protected set; }
        public int? Rating { get; protected set; }
        public int? NumOfRating { get; protected set; }
        public string Link { get; protected set; }

        public ICollection<Abonaments> Abonaments { get; set; }
        public ICollection<Events> EventsAdminNavigation { get; set; }
        public ICollection<Events> EventsWhoCreated { get; set; }
        public ICollection<Friends> FriendsFriendFromNavigation { get; set; }
        public ICollection<Friends> FriendsFriendToNavigation { get; set; }
        public ICollection<MessageEvent> MessageEventUserFrom { get; set; }
        public ICollection<MessageEvent> MessageEventUserTo { get; set; }
        public ICollection<MessageUser> MessageUserUser { get; set; }
        public ICollection<MessageUser> MessageUserUserFrom { get; set; }
        public ICollection<ObjectComments> ObjectComments { get; set; }
        public ICollection<Objects> Objects { get; set; }
        public ICollection<UsersEvents> UsersEvents { get; set; }
        public ICollection<UserComments> UserCommentsUserIdRatedNavigation { get; set; }
        public ICollection<UserComments> UserCommentsUserIdWhoCommentNavigation { get; set; }

        public void SetLogin(string login)
        {
            if (!NameRegex.IsMatch(login))
            {
                throw new Exception("Nieprawidłowa nazwa użytkownika.");
            }

            if (String.IsNullOrEmpty(login))
            {
                throw new Exception("Nieprawidłowa nazwa użytkownika.");
            }

            Login = login;
            DateLastActive = DateTime.UtcNow;
        }

        public void SetName(string name)
        {
            Name = name;
            DateLastActive = DateTime.UtcNow;
        }

        public void SetSurname(string surname)
        {
            Surname = surname;
            DateLastActive = DateTime.UtcNow;
        }

        public void SetCity(string city)
        {
            City = city;
            DateLastActive = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Nieprawidłowy adres e-mail.");
            }
            if (Email == email)
            {
                return;
            }

            Email = email;
            DateLastActive = DateTime.UtcNow;
        }

        public void SetDescription(string description)
        {
            Description = description;
            DateLastActive = DateTime.UtcNow;
        }

        public void SetRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new Exception("Rola nie może byc pusta.");
            }
            if (Role == role)
            {
                return;
            }
            Role = role;
            DateLastActive = DateTime.UtcNow;
        }
        public void SetRoleUser()
        {
            Role = "uzytkownik";
        }
        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Hasło nie może byc puste.");
            }
            if (password.Length < 4)
            {
                throw new Exception("Hasło musi składać się z minimum 4 znaków.");
            }
            if (password.Length > 100)
            {
                throw new Exception("Hasło nie może przekraczać 100 znaków.");
            }
            if (Password == password)
            {
                return;
            }
            Password = Crypto.HashPassword(password);
            DateLastActive = DateTime.UtcNow;
        }

        public void ActiveUser()
        {
            IsActive = true;
        }
        public void DisactiveUser()
        {
            IsActive = false;
        }
    }
}
