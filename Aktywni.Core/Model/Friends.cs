using System;
using System.Collections.Generic;

namespace Aktywni.Core.Model
{
    public partial class Friends
    {
        public int FriendId { get; set; }
        public int FriendFrom { get; set; }
        public int FriendTo { get; set; }
        public bool IsAccepted { get; set; }

        public Users FriendFromNavigation { get; set; }
        public Users FriendToNavigation { get; set; }
    }
}
