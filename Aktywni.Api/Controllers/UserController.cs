using Aktywni.Infrastructure.Services;
using AutoMapper.Configuration;

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

        // TODO:
        // Wyszukaj uzytkwonika o danym loginie, oceń użytkownika, przejrzyj profil
    }
}