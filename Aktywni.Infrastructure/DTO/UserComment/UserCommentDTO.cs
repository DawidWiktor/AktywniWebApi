namespace Aktywni.Infrastructure.DTO.UserComment
{
    public class UserCommentDTO
    {
        public int UserIdWhoComment { get; set; }
        public string UserLoginWhoComment { get; set; }
        public int UserIdRated { get; set; }
        public string UserLoginRated { get; set; }
        public int Rate { get; set; }
        public string Describe { get; set; }
    }
}