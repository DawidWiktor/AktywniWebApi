using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aktywni.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ApiControllerBase : Controller
    {
        protected int UserId => User?.Identity?.IsAuthenticated == true ?
            int.Parse(User.Identity.Name) :
            0;
        
    }
}