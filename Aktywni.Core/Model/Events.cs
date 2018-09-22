using System;
using System.Collections.Generic;

namespace Aktywni.Core.Model
{
    public partial class Events
    {
        public enum TypeOfVisible
        {
            W,
            N,
            U
        }

        public int EventId { get; set; }
        public int ObjectId { get; set; }
        public int WhoCreatedId { get; set; }
        public int? DisciplineId { get; set; }
        public int Admin { get; set; }
        public string Visibility { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; protected set; }
        public string GeographicalCoordinates { get; protected set; }
        public bool? Commerce { get; set; }
        public string Name { get; protected set; }
        public DateTime? CreatedDate { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }


        public Users AdminNavigation { get; set; }
        public Disciplines Discipline { get; set; }
        public Objects Object { get; set; }
        public Users WhoCreated { get; set; }
        public ICollection<UsersEvents> UsersEvents { get; set; }
        public ICollection<MessageEvent> MessageEvent { get; set; }
        public ICollection<UserComments> UserComments { get; set; }

        public Events()
        {
            MessageEvent = new HashSet<MessageEvent>();
            UsersEvents = new HashSet<UsersEvents>();
            UserComments = new HashSet<UserComments>();
        }

        public Events(string name, int objectId, DateTime date, int whoCreatedID, int admin, string description)
        {
            ObjectId = objectId;
            WhoCreatedId = whoCreatedID;
            Date = date;
            Admin = admin;
            Name = name;
            Description = description;
            CreatedDate = DateTime.Now;
            MessageEvent = new HashSet<MessageEvent>();
            UsersEvents = new HashSet<UsersEvents>();
            UserComments = new HashSet<UserComments>();
        }
 
        public Events(string name, int objectID, DateTime date, int whoCreatedID, int admin, int disciplineId, decimal latitude, decimal longitude)
        {
            ObjectId = objectID;
            WhoCreatedId = whoCreatedID;
            Date = date;
            Admin = admin;
            Name = name;
            DisciplineId = disciplineId;
            Latitude = latitude;
            Longitude = longitude;
            CreatedDate = DateTime.Now;
            MessageEvent = new HashSet<MessageEvent>();
            UsersEvents = new HashSet<UsersEvents>();
            UserComments = new HashSet<UserComments>();
        }

        public Events(string name, int objectID, DateTime date, int whoCreatedID, int admin, bool isPrivate, int disciplineId, decimal latitude, decimal longitude, string description)
        {
            ObjectId = objectID;
            WhoCreatedId = whoCreatedID;
            DisciplineId = disciplineId;
            Date = date;
            Description = description;
            Admin = admin;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            CreatedDate = DateTime.Now;
            SetVisibility(isPrivate);
            MessageEvent = new HashSet<MessageEvent>();
            UsersEvents = new HashSet<UsersEvents>();
            UserComments = new HashSet<UserComments>();
        }


        public bool SetDescritpion(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return false;

            Description = description;
            return true;
        }

        public bool SetGeographicalCoordinates(string geographicalCoordinates)
        {
            if (string.IsNullOrWhiteSpace(geographicalCoordinates))
                return false;

            GeographicalCoordinates = geographicalCoordinates;
            return true;
        }

        public bool SetGeographicalCoordinates(double latitude, double longitude)
        {
            Latitude = (decimal)latitude;
            Longitude = (decimal)longitude;
            return true;
        }


        public bool SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            Name = name;
            return true;
        }

        public bool SetVisibility(bool isPrivate)
        {
            if(isPrivate)
                Visibility = TypeOfVisible.N.ToString();
            else
                Visibility = TypeOfVisible.W.ToString();
            return true;
        }

        public bool SetVisibility(string visibility) // W - oznacza widoczny, N - niewidoczny, U - usunięty
        {
            if (string.IsNullOrWhiteSpace(visibility))
                return false;

            Visibility = visibility;
            return true;
        }

        public bool SetDate(DateTime date)
        {
            Date = date;
            return true;
        }
    }
}
