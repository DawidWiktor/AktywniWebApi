using System;
using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands.User;
using Aktywni.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aktywni.Api.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
       // [Authorize]
        public async Task<IActionResult> GetAction()
            => Json(await _userService.GetAccountAsync(UserId)); 
        
        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody]Register command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Login, command.Email, command.Password);
            return Created("/account", null);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]Login command)
            => Json(await _userService.LoginAsync(command.UserLogin, command.Password));
    }
}