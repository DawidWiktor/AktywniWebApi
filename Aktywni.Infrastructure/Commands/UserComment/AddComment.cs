namespace Aktywni.Infrastructure.Commands.UserComment
{
    public class AddComment
    {
        public int UserIdRated { get; set; }
        public int EventId { get; set; }
        public int Rate { get; set; }
        public string Describe { get; set; }
    }
}