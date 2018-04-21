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
            string connectionString = _configuration.GetConnectionString("testApi");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand com = new SqlCommand("Select count(*) from Users", connection);
            var count = (int)com.ExecuteScalar();
            System.Diagnostics.Debug.WriteLine("otrzymano : " + count);
            connection.Close();

            await _userService.RegisterAsync(Guid.NewGuid(), command.Login, command.Email, command.Password);
            return Created("/account", null);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]Login command)
            => Json(await _userService.LoginAsync(command.UserLogin, command.Password));
    }
}