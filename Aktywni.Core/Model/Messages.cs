using System;
using System.Collections.Generic;

namespace Aktywni.Core.Model
{
    public partial class Messages
    {
        public Messages()
        {
            MessageUser = new HashSet<MessageUser>();
        }

        public int MessageId { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }

        public ICollection<MessageUser> MessageUser { get; set; }
    }
}
