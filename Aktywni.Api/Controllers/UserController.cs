using System;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands;
using Aktywni.Infrastructure.Commands.User;
using Aktywni.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Aktywni.Api.Controllers
{
    public class UserController : ApiControllerBase
    {
        private IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAction() // uzyskanie informacji o sobie
           => Json(await _userService.GetAccountAsync(UserId));

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
            => Json(await _userService.GetAllUsers(UserId));

        [HttpPost("all")]
        public async Task<IActionResult> GetAllUsers([FromBody]GetListUsers command)
             => Json(await _userService.GetAllUsers(UserId, command.FragmentLogin));

        [HttpGet("profile/{userId}")]
        public async Task<IActionResult> GetUserProfile(int userId)
            => Json(await _userService.GetAccountAsync(userId));

        [HttpGet("activity/{userId}")]
        public async Task<IActionResult> GetUserActivity(int userId)
            => Json(await _userService.GetUserActivity(userId));


        [HttpGet("myActivity")]
        public async Task<IActionResult> GetMyActivity()
            => Json(await _userService.GetMyActivity(UserId));


 
        // TODO:
        // oceń użytkownika
    }
}