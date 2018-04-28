using System;
using System.Collections.Generic;

namespace Aktywni.Core.Model
{
    public partial class Users
    {
        public Users()
        {
            EventsAdminNavigation = new HashSet<Events>();
            EventsWhoCreated = new HashSet<Events>();
            FriendsFriendFromNavigation = new HashSet<Friends>();
            FriendsFriendToNavigation = new HashSet<Friends>();
            MessageUser = new HashSet<MessageUser>();
            ObjectComments = new HashSet<ObjectComments>();
            Objects = new HashSet<Objects>();
        }

        public Users(string login, string email, string password)
        {
            EventsAdminNavigation = new HashSet<Events>();
            EventsWhoCreated = new HashSet<Events>();
            FriendsFriendFromNavigation = new HashSet<Friends>();
            FriendsFriendToNavigation = new HashSet<Friends>();
            MessageUser = new HashSet<MessageUser>();
            ObjectComments = new HashSet<ObjectComments>();
            Objects = new HashSet<Objects>();   
            Login = login;
            Email = email;
            Password = password;
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
        public string Describe { get; protected set; }
        public int? Rating { get; protected set; }
        public int? NumOfRating { get; protected set; }
        public string Link { get; protected set; }

        public ICollection<Events> EventsAdminNavigation { get; set; }
        public ICollection<Events> EventsWhoCreated { get; set; }
        public ICollection<Friends> FriendsFriendFromNavigation { get; set; }
        public ICollection<Friends> FriendsFriendToNavigation { get; set; }
        public ICollection<MessageUser> MessageUser { get; set; }
        public ICollection<ObjectComments> ObjectComments { get; set; }
        public ICollection<Objects> Objects { get; set; }

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
