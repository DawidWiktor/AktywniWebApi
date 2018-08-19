using System;
using System.Collections.Generic;

namespace Aktywni.Core.Model
{
    public partial class Events
    {
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

        public Events(int objectId, int whoCreatedId, int admin, string name)
        {
            ObjectId = objectId;
            WhoCreatedId = whoCreatedId;
            Admin = admin;
            Name = name;
        }

        public Events(int objectId, int whoCreatedId, int disciplineId, DateTime date, string description, int admin, string name)
        {
            ObjectId = objectId;
            WhoCreatedId = whoCreatedId;
            DisciplineId = disciplineId;
            Date = date;
            Description = description;
            Admin = admin;
            Name = name;
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
    }
}
