using System;

namespace Aktywni.Infrastructure.DTO.UserEvent
{
    public class HistoryDTO
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime Date { get; set; }
        public int DisciplineID { get; set; }
        public string Description { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

    }
}