using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands.MessageUser;
using Aktywni.Infrastructure.Services;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Aktywni.Api.Controllers
{
    public class MessageUserController : ApiControllerBase
    {
        private IMessageUserService _messageUserService;
        //    private readonly IConfiguration _configuration;
        public MessageUserController(IMessageUserService messageUserService)
        {
            _messageUserService = messageUserService;
            //   _configuration = configuration;
        }

        [HttpGet("headers")]
        public async Task<IActionResult> GetAllHeadersMessages()
            => Json(await _messageUserService.GetAllHeaderMessagesUsers(UserId));

        [HttpPut("latest")]
        public async Task<IActionResult> GetLatestMessageInFriend([FromBody]GetMessagesInFriend command)
            => Json(await _messageUserService.GetLatestMessagesInFriend(UserId, command.FriendId));

        [HttpPut("unread")]
        public async Task<IActionResult> GetUnreadMessagesInFriend([FromBody]GetMessagesInFriend command)
            => Json(await _messageUserService.GetLatestMessagesInFriend(UserId, command.FriendId));
        
        
         [HttpPut("history")]
        public async Task<IActionResult> GetHistoryMessagesInFriend([FromBody]GetHistoryMessagesInFriend command)
            => Json(await _messageUserService.GetHistoryMessagesInFriend(UserId, command.FriendId, command.LatestMessageId));
        
        

        [HttpPost("send")]
        public async Task<IActionResult> SendMessageToUser([FromBody]SendMessageUser command)
            => Json(await _messageUserService.SendMessageAsync(UserId, command.UserToId, command.Content));
    }
}