namespace Aktywni.Core.Model
{
    public partial class UserComments
    {
        public int UserCommentId { get; set; }
        public int UserIdWhoComment { get; set; }
        public int UserIdRated { get; set; }
        public int Rate { get; set; }
        public string Describe { get; set; }

        public Users UserIdRatedNavigation { get; set; }
        public Users UserIdWhoCommentNavigation { get; set; }

        public UserComments(){

        }

        public UserComments(int myId, int userIdRated, int rate, string describe)
        {
            UserIdWhoComment = myId;
            UserIdRated = userIdRated;
            Rate = rate;
            Describe = describe;   
        }
    }
}