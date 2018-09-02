using System;
using System.Collections.Generic;

namespace Aktywni.Core.Model
{
    public partial class Messages
    {
        public Messages()
        {
            MessageEvent = new HashSet<MessageEvent>();
            MessageUser = new HashSet<MessageUser>();
        }

        public int MessageId { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }

        public ICollection<MessageUser> MessageUser { get; set; }
        public ICollection<MessageEvent> MessageEvent { get; set; }

        public Messages(string content, DateTime date)
        {
            SetContent(content);
            SetDate(date);
        }

        public bool SetContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return false;

            Content = content;
            return true;
        }

        public bool SetDate(DateTime date)
        {
            Date = date;
            return true;
        }
    }
}
