namespace Aktywni.Infrastructure.DTO
{
    public class FriendDTO
    {
        public int FriendUserId {get; set;}
        public string Email {get; set;}
        public string Login {get; set;} 
        public string Name {get; set;}
        public string Surname {get; set;}
        public string City {get; set;}
        public string Describe {get; set;}
        public decimal Rate {get; set;}
        public bool IsAccepted {get; set;}
    }
}