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
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]Register command)
            => Json(await _userService.RegisterAsync(command.Login, command.Email, command.Password));

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]Login command)
        {
            Console.WriteLine("Login: " + command.UserLogin + " hasło: " + command.Password);
            return Json(await _userService.LoginAsync(command.UserLogin, command.Password));
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
            => Json(await _userService.LogoutAsync(UserId));

        [HttpPut("changeEmail")]
        public async Task<IActionResult> ChangeEmail([FromBody]ChangeEmail command)
            => Json(await _userService.ChangeEmailAsync(UserId, command.Email));


        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePassword command)
            => Json(await _userService.ChangePasswordAsync(UserId, command.CurrentPassword, command.NewPassword));

        [HttpPut("changePersonalData")]
        public async Task<IActionResult> ChangePersonalData([FromBody]ChangePersonalData command)
            => Json(await _userService.ChangePersonalDataAsync(UserId, command.Name, command.Surname, command.City));

        [HttpPut("changeDescription")]
        public async Task<IActionResult> ChangeDescription([FromBody]ChangeDescription command)
            => Json(await _userService.ChangeDescriptionAsync(UserId, command.Description));

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAccount()
            => Json(await _userService.RemoveAccountAsync(UserId));

    }
}