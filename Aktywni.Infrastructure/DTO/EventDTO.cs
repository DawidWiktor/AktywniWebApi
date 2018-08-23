using System;

namespace Aktywni.Infrastructure.DTO
{
    public class EventDTO
    {
        public int EventId { get; set; }
        public int ObjectId { get; set; }
        public int WhoCreatedId { get; set; }
        public int? DisciplineId { get; set; }
        public int Admin { get; set; }
        public string Visibility { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; protected set; }
        public string GeographicalCoordinates { get; protected set; }
        public bool? Commerce { get; set; }
        public string Name { get; protected set; }
    }
}