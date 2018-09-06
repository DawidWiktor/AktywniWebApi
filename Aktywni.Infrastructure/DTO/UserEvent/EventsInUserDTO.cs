using System;

namespace Aktywni.Infrastructure.DTO.UserEvent
{
    public class EventsInUserDTO
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime Date { get; set; }
        public bool IsAccepted { get; set; }
    }
}