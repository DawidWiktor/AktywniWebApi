using System;
using System.Collections.Generic;

namespace Aktywni.Core.Model
{
    public partial class Friends
    {
        public int FriendId { get; protected set; }
        public int FriendFrom { get; protected set; }
        public int FriendTo { get; protected set; }
        public bool IsAccepted { get; protected set; }

        public Users FriendFromNavigation { get; set; }
        public Users FriendToNavigation { get; set; }

        public Friends()
        {
            
        }
        public Friends(int friendFrom, int friendTo, bool isAccepted)
        {
            if(friendFrom != friendTo)
            {
                FriendFrom = friendFrom;
                FriendTo = friendTo;
                IsAccepted = isAccepted;
            }
        }

        public void AcceptInvitation()
        {
            IsAccepted = true;
        }

        

    }
}
