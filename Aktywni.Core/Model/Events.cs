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

        public Users AdminNavigation { get; set; }
        public Disciplines Discipline { get; set; }
        public Objects Object { get; set; }
        public Users WhoCreated { get; set; }
        public ICollection<UsersEvents> UsersEvents { get; set; }

        public Events()
        {
        }

        public Events(string name, int objectId,  DateTime date, int whoCreatedID, int admin, string description)
        {
            ObjectId = objectId;
            WhoCreatedId = whoCreatedID;
            Date = date;
            Admin = admin;
            Name = name;
            Description = description;
        }

        public Events(string name, int objectID, DateTime date, int whoCreatedID, int admin, int disciplineId, string geographicalCoordinates)
        {
            ObjectId = objectID;
            WhoCreatedId = whoCreatedID;
            Date = date;
            Admin = admin;
            Name = name;
            DisciplineId = disciplineId;
            GeographicalCoordinates = geographicalCoordinates;
        }
        
        public Events(string name, int objectID, DateTime date, int whoCreatedID, int admin, int disciplineId, string geographicalCoordinates, string description)
        {
            ObjectId = objectID;
            WhoCreatedId = whoCreatedID;
            DisciplineId = disciplineId;
            Date = date;
            Description = description;
            Admin = admin;
            Name = name;
            GeographicalCoordinates = geographicalCoordinates;
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

        public bool SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            Name = name;
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
