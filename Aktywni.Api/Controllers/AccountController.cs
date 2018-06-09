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
        public async Task<IActionResult> Post([FromBody]Register command)
        {
            bool isRegistered  = await _userService.RegisterAsync(command.Login, command.Email, command.Password);
            return Json(new ReturnResponse{Response = isRegistered.ToString()});
        }
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]Login command)
        {
            Console.WriteLine("Login: "+ command.UserLogin + " hasło: " + command.Password);
            return Json(await _userService.LoginAsync(command.UserLogin, command.Password));
        }
        
        [HttpPut("changeEmail")]
        [Authorize]
        public async Task<IActionResult> Put([FromBody]ChangeEmail command)
        {
            await _userService.ChangeEmailAsync(UserId, command.Email);
            return NoContent();
        }

        [HttpPut("changePassword")]
        [Authorize]
        public async Task<IActionResult> Put([FromBody]ChangePassword command)
        {
            await _userService.ChangePasswordAsync(UserId, command.CurrentPassword, command.NewPassword);
            return NoContent();
        }

        [HttpPut("changePersonalData")]
        [Authorize]
        public async Task<IActionResult> Put([FromBody]ChangePersonalData command)
        {
            await _userService.ChangePersonalDataAsync(UserId, command.Name, command.Surname, command.City);
            return NoContent();
        }

        [HttpPut("changeDescription")]
        [Authorize]
        public async Task<IActionResult> Put([FromBody]ChangeDescription command)
        {
            await _userService.ChangeDescriptionAsync(UserId, command.Description);
            return NoContent();
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<IActionResult> delete()
        {
            await _userService.RemoveAccountAsync(UserId);
            return NoContent();
        }
    }
}