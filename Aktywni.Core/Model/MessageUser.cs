using System;
using System.Collections.Generic;

namespace Aktywni.Core.Model
{
    public partial class MessageUser
    {
        public int MessageUserId { get; set; }
        public int UserId { get; set; }
        public int MessageId { get; set; }
        public bool IsOpened { get; set; }
        public int? UserFromId { get; set; }

        public Messages Message { get; set; }
        public Users User { get; set; }
        public Users UserFrom { get; set; }

        public MessageUser()
        {
        }


        //public MessageUser(int userId, )
    }
}
