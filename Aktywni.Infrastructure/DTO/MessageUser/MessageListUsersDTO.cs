using Newtonsoft.Json;

namespace Aktywni.Infrastructure.DTO.MessageUser
{
    public class MessageListUsersDTO
    {
        [JsonProperty("UserFromId")]
        public int UserFromId { get; set; }
        [JsonProperty("UserLoginFrom")]
        public string UserLoginFrom { get; set; }
    }
}