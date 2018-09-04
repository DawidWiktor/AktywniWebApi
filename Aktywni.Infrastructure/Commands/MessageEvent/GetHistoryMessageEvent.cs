namespace Aktywni.Infrastructure.Commands.MessageEvent
{
    public class GetHistoryMessageEvent
    {
        public int EventId { get; set; }
        public int LatestMessageId { get; set; }
    }
}