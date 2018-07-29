using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands;
using Aktywni.Infrastructure.Commands.Object;
using Aktywni.Infrastructure.Commands.User;
using Aktywni.Infrastructure.DTO;
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

        [HttpGet("{objID}")]
        public async Task<IActionResult> Get(int objID)
             => Json(await _objectService.GetObjectAsync(objID));
        
        [HttpGet("{objID}")]
        public async Task<IActionResult> Get(string name)
            => Json(await _objectService.GetObjectAsync(name));

        [HttpGet]
        public async Task<IActionResult> GetAction()                    // wszystkie obiekty
           => Json(await _objectService.GetAllObjectsAsync());

        [HttpPost("searchObject")]
        public async Task<IActionResult> SearchObject([FromBody]SearchObject command)
        {
            var response = await _objectService.SearchObjectsAsync(command.Name);
            return Json(response);
        }


        [HttpPost("addObject")]
        public async Task<IActionResult> AddObject([FromBody]AddObject command)
        {
            var response = await _objectService.AddObjectAsync(UserId, command.Name, command.City, command.Street, command.Street, command.GeographicalCoordinates);
            return Json(new ReturnResponse{ Response = response.Item1.ToString(), Error = response.Item2});
        }
    }
}