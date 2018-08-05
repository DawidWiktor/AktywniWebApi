using System;
using System.Collections.Generic;

namespace Aktywni.Core.Model
{
    public partial class Disciplines
    {
        public Disciplines()
        {
            Events = new HashSet<Events>();
        }

        public Disciplines(string name, string description)
        {
            SetName(name);
            SetDescription(description);
        }

        public int DisciplineId { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Visibility { get; protected set; }
        public ICollection<Events> Events { get; set; }

        public bool SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            Name = name;
            return true;
        }

        public bool SetDescription(string description)
        {
            Description = description;
            return true;
        }
    }
}
