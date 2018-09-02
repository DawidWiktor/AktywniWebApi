namespace Aktywni.Infrastructure.Commands.MessageUser
{
    public class GetHistoryMessagesInFriend
    {
        public int FriendId { get; set; }
        public int LatestMessageId { get; set; }
    }
}