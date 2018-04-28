using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands.User;
using Aktywni.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
namespace Aktywni.Api.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private IUserService _userService;
        private readonly IConfiguration _configuration;
        public AccountController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAction()
            => Json(await _userService.GetAccountAsync(UserId)); 
        
        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody]Register command)
        {
            await _userService.RegisterAsync(command.Login, command.Email, command.Password);
            return Created("/account", null);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]Login command)
            => Json(await _userService.LoginAsync(command.UserLogin, command.Password));
        
        [HttpPut("changeEmail")]
        public async Task<IActionResult> Put([FromBody]ChangeEmail command)
        {
            await _userService.ChangeEmailAsync(UserId, command.Email);
            return NoContent();
        }

        [HttpPut("changePassword")]
        public async Task<IActionResult> Put([FromBody]ChangePassword command)
        {
            await _userService.ChangePasswordAsync(UserId, command.CurrentPassword, command.NewPassword);
            return NoContent();
        }

        [HttpPut("changePersonalData")]
        public async Task<IActionResult> Put([FromBody]ChangePersonalData command)
        {
            await _userService.ChangePersonalDataAsync(UserId, command.Name, command.Surname, command.City);
            return NoContent();
        }

        [HttpPut("changeDescription")]
        public async Task<IActionResult> Put([FromBody]ChangeDescription command)
        {
            await _userService.ChangeDescription(UserId, command.Description);
            return NoContent();
        }
    }
}