using System;
using Newtonsoft.Json;

namespace Aktywni.Infrastructure.DTO.MessageEventUser
{
    public class MessageEventListUserDTO
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime Date { get; set; }
    }
}