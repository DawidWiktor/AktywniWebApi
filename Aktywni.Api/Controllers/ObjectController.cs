using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands.Object;
using Aktywni.Infrastructure.Commands.User;
using Aktywni.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Aktywni.Api.Controllers
{
    public class ObjectController : ApiControllerBase
    {
        private IObjectService _objectService;
        private readonly IConfiguration _configuration;
        public ObjectController(IObjectService objectService, IConfiguration configuration)
        {
            _objectService = objectService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAction()                    // wszystkie obiekty
           => Json(await _objectService.GetAllObjectsAsync());

        [HttpGet("{objID}")]
        public async Task<IActionResult> Get(int objID)
             => Json(await _objectService.GetObjectAsync(objID));
        

    }
}