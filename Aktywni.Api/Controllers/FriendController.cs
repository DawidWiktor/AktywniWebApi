using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands.Friend;
using Aktywni.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Aktywni.Api.Controllers
{
    [Authorize]
    public class FriendController : ApiControllerBase
    {
        private IFriendService _friendService;
        private readonly IConfiguration _configuration;
        public FriendController(IFriendService friendService, IConfiguration configuration)
        {
            _friendService = friendService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get()                    // wszyscy znajomi
            => Json(await _friendService.GetAllFriendsAsync(UserId));

        [HttpGet("{friendID}")]
        public async Task<IActionResult> Get(int friendID)
            => Json(await _friendService.GetFriendAsync(UserId, friendID));

        [HttpPost("addFriend")]
        public async Task<IActionResult> Post([FromBody]AddFriend command)
        {
            await _friendService.AddFriendAsync(UserId, command.friendID);
            return Created("/friend", null);
        }

        [HttpPut("{friendID}")]
        public async Task<IActionResult> Put(int friendID)
        {
            await _friendService.AcceptInvitationAsync(UserId, friendID);
            return NoContent();
        }

        [HttpDelete("{friendID}")]
        public async Task<IActionResult> Delete(int friendID)
        {
            await _friendService.RemoveFriendAsync(UserId, friendID);
            return NoContent();
        }
    }
}