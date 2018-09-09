using System;

namespace Aktywni.Infrastructure.Commands.User
{
    public class UserActivity
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime Date { get; set; }
    }
}