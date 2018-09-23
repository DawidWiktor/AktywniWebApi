using System;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands;
using Aktywni.Infrastructure.Commands.User;
using Aktywni.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Aktywni.Api.Controllers
{
     [Route("[controller]")]
    public class WebsiteController : Controller
    {
        private IEventService _eventService;
        private readonly IConfiguration _configuration;
        public WebsiteController(IEventService eventService, IConfiguration configuration)
        {
            _eventService = eventService;
            _configuration = configuration;
        }

        /*       [HttpGet("{eventId}")]
               public async Task<IActionResult> GetEvent(int eventId)
                   => Json(await _eventService.GetEventAsync(eventId, 7));*/
        [HttpGet]
        public HttpResponseMessage Index()
        {
            var response = new HttpResponseMessage();
            response.Content = new StringContent("<html><body>Hello World</body></html>");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

      [HttpGet("Ab")]
        public async Task<IActionResult> About()
        {
            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string path1 = System.IO.Directory.GetCurrentDirectory() + @"\Views\SignIn.cshtml";
            return View(path1);
           return Content("<html><body>Hello World"+ path1  +"</body></html>","text/html");
        }

        
    }
}