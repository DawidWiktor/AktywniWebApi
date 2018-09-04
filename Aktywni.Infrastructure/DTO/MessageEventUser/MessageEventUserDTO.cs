using System;

namespace Aktywni.Infrastructure.DTO.MessageEventUser
{
    public class MessageEventUserDTO
    {
        public int EventId { get; set; }
        public int MessageId { get; set; }
        public string EventName { get; set; }
        public string Login { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
    }
}