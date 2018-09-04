namespace Aktywni.Infrastructure.Commands.MessageEvent
{
    public class SendMessageToEvent
    {
        public int EventId { get; set; }
        public string Content { get; set; }
    }
}