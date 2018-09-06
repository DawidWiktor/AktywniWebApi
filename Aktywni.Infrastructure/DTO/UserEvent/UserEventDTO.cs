namespace Aktywni.Infrastructure.DTO.UserEvent
{
    public class UserEventDTO
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public string Login { get; set; }
        public bool IsAccepted { get; set; }
    }
}