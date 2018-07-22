using System;
using System.Data.SqlClient;
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
        public async Task<IActionResult> Register([FromBody]Register command)
        {
            bool isRegistered = await _userService.RegisterAsync(command.Login, command.Email, command.Password);
            return Json(new ReturnResponse { Response = isRegistered.ToString() });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]Login command)
        {
            Console.WriteLine("Login: " + command.UserLogin + " hasło: " + command.Password);
            var isLogged = await _userService.LoginAsync(command.UserLogin, command.Password);
           
            if(isLogged == null)
                return Json(new ReturnResponse { Response = "False"});
            else
                return Json(isLogged);
        }

        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            bool isLogout = await _userService.LogoutAsync(UserId); 
            return Json(new ReturnResponse { Response = isLogout.ToString() });
        }

        [HttpPut("changeEmail")]
        [Authorize]
        public async Task<IActionResult> ChangeEmail([FromBody]ChangeEmail command)
        {
            string isChanged = await _userService.ChangeEmailAsync(UserId, command.Email);
            return Json(new ReturnResponse { Response = isChanged });
        }

        [HttpPut("changePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePassword command)
        {
            string isChanged = await _userService.ChangePasswordAsync(UserId, command.CurrentPassword, command.NewPassword);
            return Json(new ReturnResponse { Response = isChanged });
        }

        [HttpPut("changePersonalData")]
        [Authorize]
        public async Task<IActionResult> ChangePersonalData([FromBody]ChangePersonalData command)
        {
            string isChanged = await _userService.ChangePersonalDataAsync(UserId, command.Name, command.Surname, command.City);
            return Json(new ReturnResponse { Response = isChanged });
        }

        [HttpPut("changeDescription")]
        [Authorize]
        public async Task<IActionResult> ChangeDescription([FromBody]ChangeDescription command)
        {
            string isChanged = await _userService.ChangeDescriptionAsync(UserId, command.Description);
            return Json(new ReturnResponse { Response = isChanged });
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<IActionResult> DeleteAccount()
        {
            string isChanged = await _userService.RemoveAccountAsync(UserId);
            return Json(new ReturnResponse { Response = isChanged });
        }
    }
}