using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands;
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
        public async Task<IActionResult> GetAllFriends()                    // wszyscy znajomi
            => Json(await _friendService.GetAllFriendsAsync(UserId));

        [HttpGet("{friendID}")]                                             // konkretny znajomy
        public async Task<IActionResult> GetFriend(int friendID)
            => Json(await _friendService.GetFriendAsync(UserId, friendID));

        [HttpPost("search")]
        public async Task<IActionResult> SearchFriend([FromBody]SearchFriend command)
            => Json(await _friendService.SearchFriendsAsync(UserId, command.TextInput));

        [HttpPost("addFriend")]
        public async Task<IActionResult> AddFriend([FromBody]AddFriend command)
            => Json(await _friendService.AddFriendAsync(UserId, command.friendID));

        [HttpPut("{friendID}")]
        public async Task<IActionResult> AcceptInvitationFromFriend(int friendID)
            => Json(await _friendService.AcceptInvitationAsync(UserId, friendID));

        [HttpDelete("{friendID}")]
        public async Task<IActionResult> DeleteFriend(int friendID)
            =>Json(await _friendService.RemoveFriendAsync(UserId, friendID));
    }
}