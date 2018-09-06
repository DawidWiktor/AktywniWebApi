using System;
using System.Collections.Generic;

namespace Aktywni.Core.Model
{
    public partial class UsersEvents
    {
        public int UserEventId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public bool? IsAccepted { get; set; }

        public Events Event { get; set; }
        public Users User { get; set; }

        public UsersEvents()
        {
        }

        public UsersEvents(int eventId, int userId, bool isAccepted = false)
        {
            EventId = eventId;
            UserId = userId;
            IsAccepted = isAccepted;
        }
    }
}
