using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Infrastructure.Commands;
using Aktywni.Infrastructure.Commands.UserComment;
using Aktywni.Infrastructure.Commands.UserEvent;
using Aktywni.Infrastructure.DTO;
using Aktywni.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace Aktywni.Api.Controllers
{
    public class UserCommentController : ApiControllerBase
    {
        private readonly IUserCommentService _userCommentService;
        private readonly IConfiguration _configuration;
        public UserCommentController(IUserCommentService userCommentService, IConfiguration configuration)
        {
            _userCommentService = userCommentService;
            _configuration = configuration;
        }

        [HttpGet("myComments")]
        public async Task<IActionResult> GetMyComments() // uzyskanie przeze mnie napisanych komentarzy
           => Json(await _userCommentService.GetMyComments(UserId));

        [HttpGet("user/{userId}")]          // uzyskanie komentarzy użytkownika
        public async Task<IActionResult> GetUserComments(int userId)
            => Json(await _userCommentService.GetUserComments(userId));

        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetCommentsInEvent(int eventId)
            => Json(await _userCommentService.GetCommentsInEvent(UserId, eventId));

        [HttpPost("add")]
        public async Task<IActionResult> AddComment([FromBody]AddComment command)
            => Json(await _userCommentService.AddComment(UserId, command.UserIdRated, command.EventId, command.Rate, command.Describe));

        [HttpPut("update")]
        public async Task<IActionResult> UpdateComment([FromBody]AddComment command)
            => Json(await _userCommentService.UpdateComment(UserId, command.UserIdRated, command.EventId, command.Rate, command.Describe));

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveComment([FromBody]RemoveComment command)
            => Json(await _userCommentService.RemoveComment(UserId, command.UserIdRated, command.EventId));
    }
}