using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands;
using Aktywni.Infrastructure.Commands.MessageEvent;
using Aktywni.Infrastructure.Commands.Object;
using Aktywni.Infrastructure.Commands.User;
using Aktywni.Infrastructure.DTO;
using Aktywni.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Aktywni.Api.Controllers
{
    public class MessageEventController : ApiControllerBase
    {
         private IMessageEventService _messageEventService;
        private readonly IConfiguration _configuration;
        public MessageEventController(IMessageEventService messageEventService, IConfiguration configuration)
        {
            _messageEventService = messageEventService;
            _configuration = configuration;
        }

        [HttpGet("headers")]
        public async Task<IActionResult> GetAllHeadersMessages()
            => Json(await _messageEventService.GetAllHeaderMessageEvent(UserId));

        [HttpPut("latest")]
        public async Task<IActionResult> GetLatestMessageInEvent([FromBody]GetMessageEvent command)
            => Json(await _messageEventService.GetLatestMessagesInEvent(UserId, command.EventId));

        [HttpPut("unread")]
        public async Task<IActionResult> GetUnreadMessagesInEvent([FromBody]GetMessageEvent command)
            => Json(await _messageEventService.GetUnreadMessagesInEvent(UserId, command.EventId));
        
         [HttpPut("history")]
        public async Task<IActionResult> GetHistoryMessagesInEvent([FromBody]GetHistoryMessageEvent command)
            => Json(await _messageEventService.GetHistoryMessagesInEvent(UserId, command.EventId, command.LatestMessageId));

        [HttpPost("send")]
        public async Task<IActionResult> SendMessageToEvent([FromBody]SendMessageToEvent command)
            => Json(await _messageEventService.SendMessageAsync(UserId, command.EventId, command.Content));
    
    }
}