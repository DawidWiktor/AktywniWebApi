using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands;
using Aktywni.Infrastructure.Commands.MessageUser;
using Aktywni.Infrastructure.Commands.Object;
using Aktywni.Infrastructure.Commands.User;
using Aktywni.Infrastructure.DTO;
using Aktywni.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Aktywni.Api.Controllers
{
    public class MessageUserController : ApiControllerBase
    {
        private IMessageUserService _messageUserService;
            private readonly IConfiguration _configuration;
        public MessageUserController(IMessageUserService messageUserService, IConfiguration configuration)
        {
            _messageUserService = messageUserService;
            _configuration = configuration;
        }

        [HttpGet("headers")]
        public async Task<IActionResult> GetAllHeadersMessages()
            => Json(await _messageUserService.GetAllHeaderMessagesUsers(UserId));

        [HttpPut("latest")]
        public async Task<IActionResult> GetLatestMessageInFriend([FromBody]GetMessagesInFriend command)
            => Json(await _messageUserService.GetLatestMessagesInFriend(UserId, command.FriendId));

        [HttpPut("unread")]
        public async Task<IActionResult> GetUnreadMessagesInFriend([FromBody]GetMessagesInFriend command)
            => Json(await _messageUserService.GetUnreadMessagesInFriend(UserId, command.FriendId));
        
         [HttpPut("history")]
        public async Task<IActionResult> GetHistoryMessagesInFriend([FromBody]GetHistoryMessagesInFriend command)
            => Json(await _messageUserService.GetHistoryMessagesInFriend(UserId, command.FriendId, command.LatestMessageId));
        
        [HttpPost("send")]
        public async Task<IActionResult> SendMessageToUser([FromBody]SendMessageUser command)
            => Json(await _messageUserService.SendMessageAsync(UserId, command.UserToId, command.Content));
    }
}