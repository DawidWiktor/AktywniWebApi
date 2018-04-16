using System;
using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands.User;
using Aktywni.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aktywni.Api.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
      //  public Task<IActionResult> Get() => Json(new User(123, ""));
        
        [HttpPost("register")]
        public async Task<IActionResult> Post(Register command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Login, command.Email, command.Password);
            return Created("/account", null);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Post(Login command)
            => Json(await _userService.LoginAsync(command.UserLogin, command.Password))
    }
}