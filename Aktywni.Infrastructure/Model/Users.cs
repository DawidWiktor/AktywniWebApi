using System;
using System.Collections.Generic;

namespace Aktywni.Infrastructure.Model
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

        public int UserId { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateLastActive { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Role { get; set; }
        public string Describe { get; set; }
        public int? Rating { get; set; }
        public int? NumOfRating { get; set; }
        public string Link { get; set; }

        public ICollection<Events> EventsAdminNavigation { get; set; }
        public ICollection<Events> EventsWhoCreated { get; set; }
        public ICollection<Friends> FriendsFriendFromNavigation { get; set; }
        public ICollection<Friends> FriendsFriendToNavigation { get; set; }
        public ICollection<MessageUser> MessageUser { get; set; }
        public ICollection<ObjectComments> ObjectComments { get; set; }
        public ICollection<Objects> Objects { get; set; }
    }
}
