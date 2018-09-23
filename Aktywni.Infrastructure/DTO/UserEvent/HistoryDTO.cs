using System;

namespace Aktywni.Infrastructure.DTO.UserEvent
{
    public class HistoryDTO
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime Date { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

    }
}