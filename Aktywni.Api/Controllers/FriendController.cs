using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands.User;
using Aktywni.Infrastructure.Services;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAction()                    // wszyscy znajomi
            => Json(await _friendService.GetAllFriendsAsync(UserId)); 
        
        [HttpGet]
        public async Task<IActionResult> GetAction([FromBody]AddFriend command)
            => Json(await _friendService.GetFriendAsync(UserId, command.friendID));
        
        [HttpPost("addFriend")]
        public async Task<IActionResult> Post([FromBody]AddFriend command)
        {
            await _friendService.AddFriendAsync(UserId, command.friendID);
            return Created("/friend", null);
        }

        [HttpPut("acceptInvitation")]
        public async Task<IActionResult> Put([FromBody]AddFriend command)
        {
            await _friendService.AcceptInvitationAsync(UserId, command.friendID);
            return NoContent();
        }

        [HttpDelete("deleteFriend")]
        public async Task<IActionResult> Delete([FromBody]AddFriend command)
        {
            await _friendService.RemoveFriendAsync(UserId, command.friendID);
            return NoContent();
        }
    }
}