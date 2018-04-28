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

        public int DisciplineId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Events> Events { get; set; }
    }
}
