using System;
using System.Collections.Generic;

namespace Aktywni.Infrastructure.Model
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
        public string Description { get; set; }

        public Users AdminNavigation { get; set; }
        public Disciplines Discipline { get; set; }
        public Objects Object { get; set; }
        public Users WhoCreated { get; set; }
    }
}
