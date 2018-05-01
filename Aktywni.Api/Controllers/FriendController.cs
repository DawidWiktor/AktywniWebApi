using Aktywni.Infrastructure.Services;
using AutoMapper.Configuration;

namespace Aktywni.Api.Controllers
{
    public class FriendController : ApiControllerBase
    {
        private IUserService _userService;
        private readonly IConfiguration _configuration;
        public FriendController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        
    }
}