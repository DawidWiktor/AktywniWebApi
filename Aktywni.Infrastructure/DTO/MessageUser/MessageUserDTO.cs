using System;

namespace Aktywni.Infrastructure.DTO.MessageUser
{
    public class MessageUserDTO
    {
        public int UserFromId { get; set; }
        public int UserId { get; set; }
        public int MessageId { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
    }
}