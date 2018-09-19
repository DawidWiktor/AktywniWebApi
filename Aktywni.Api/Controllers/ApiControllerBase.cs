using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        protected bool IsCommerceUser
        {
            get
            {
                var identity = HttpContext?.User?.Claims;
                if(identity != null)
                    {
                        if(identity.Where(x=>x.Type == ClaimTypes.Role).FirstOrDefault().Value == "biznes" || identity.Where(x=>x.Type == ClaimTypes.Role).FirstOrDefault().Value == "admin")
                            return true;
                    }
                return false;
            }
        }
    }
}