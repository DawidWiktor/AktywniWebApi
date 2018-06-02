using System;
using Microsoft.AspNetCore.Mvc;

namespace Aktywni.Api.Controllers
{
    [Route("[controller]")]
    public class ApiControllerBase : Controller
    {
        protected int UserId => User?.Identity?.IsAuthenticated == true ?
            int.Parse(User.Identity.Name) :
            0;
        
    }
}