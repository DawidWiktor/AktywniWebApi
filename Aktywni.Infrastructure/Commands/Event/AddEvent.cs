using System;

namespace Aktywni.Infrastructure.Commands.Event
{
    public class AddEvent
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int DisciplineId { get; set; }
        public string Description { get; set; }
        public string GeographicalCoordinates { get; set; }
    }
}