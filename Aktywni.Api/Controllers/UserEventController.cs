using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands;
using Aktywni.Infrastructure.Commands.UserEvent;
using Aktywni.Infrastructure.DTO;
using Aktywni.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Aktywni.Api.Controllers
{
    public class UserEventController : ApiControllerBase
    {
        private IUserEventService _userEventService;
        private readonly IConfiguration _configuration;
        public UserEventController(IUserEventService userEventService, IConfiguration configuration)
        {
            _userEventService = userEventService;
            _configuration = configuration;
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetUsersInEvent(int eventId)
            => Json(await _userEventService.GetUsersInEventAsync(eventId));

        [HttpPost("add")]
        public async Task<IActionResult> AddUserInEvent([FromBody]AddUserInEvent Command)
            => Json(await _userEventService.AddUserInEvent(Command.EventId, Command.UserId));
        
        [HttpDelete("remowe")]
        public async Task<IActionResult> RemoveUserInEvent([FromBody]AddUserInEvent Command)
            => Json(await _userEventService.RemoveUserInEvent(Command.EventId, Command.UserId));
        
    }
}