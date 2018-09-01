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
            => Json(await _messageUserService.GetAllHeaderMessageUsers(UserId));

        [HttpPut("latest")]
        public async Task<IActionResult> GetLatestMessageInFriend([FromBody]GetMessagesInFriend command)
            => Json(await _messageUserService.GetLatestMessageInFriend(UserId, command.FriendId));

        [HttpPost("send")]
        public async Task<IActionResult> SendMessageToUser([FromBody]SendMessageUser command)
            => Json(await _messageUserService.SendMessageAsync(UserId, command.UserToId, command.Content));
    }
}