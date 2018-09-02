namespace Aktywni.Core.Model
{
    public partial class MessageEvent
    {
        public int MessageEventId { get; set; }
        public int EventId { get; set; }
        public int MessageId { get; set; }
        public int UserFromId { get; set; }
        public bool IsOpened { get; set; }
        public int? UserToId { get; set; }

        public Events Event { get; set; }
        public Messages Message { get; set; }
        public Users UserFrom { get; set; }
        public Users UserTo { get; set; }

    }
}