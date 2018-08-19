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

        [HttpGet("{objectId}")]
        public async Task<IActionResult> GetObject(int objectId)
             => Json(await _objectService.GetObjectAsync(objectId));

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetObject(string name)
            => Json(await _objectService.GetObjectAsync(name));

        [HttpGet]
        public async Task<IActionResult> GetAllObjects()                    // wszystkie obiekty
           => Json(await _objectService.GetAllObjectsAsync());

        [HttpPost("search")]
        public async Task<IActionResult> SearchObjects([FromBody]SearchObject command)
            => Json(await _objectService.SearchObjectsAsync(command.Name));
        
        [HttpPost("searchInCity")]
        [HttpPost]
        public async Task<IActionResult> SearchObjects([FromBody]SearchObjectInCity command)
            => Json(await _objectService.SearchObjectsInCityAsync(command.Name, command.City));

        [HttpPost("add")]
        public async Task<IActionResult> AddObject([FromBody]AddObject command)
            => Json(await _objectService.AddObjectAsync(UserId, command.Name, command.City,
                        command.Street, command.PostCode, command.GeographicalCoordinates));

        [HttpPut("changeName/{objectId}")]
        public async Task<IActionResult> ChangeNameObject(int objectId, [FromBody]ChangeNameObject command)
            => Json(await _objectService.ChangeNameObjectAsync(objectId, command.NewName));

         [HttpPut("changeAddress/{objectId}")]
        public async Task<IActionResult> ChangeAddressObject(int objectId, [FromBody]ChangeAddressObject command)
            => Json(await _objectService.ChangeAddressObjectAsync(objectId, command.City, command.Street,
                        command.Postcode, command.GeographicalCoordinates));

        [HttpPut("rate/{objectId}")]
        public async Task<IActionResult> RateObject(int objectId, [FromBody]RateObject command)
            => Json(await _objectService.RateObject(objectId, command.Rate));   
        
        [HttpDelete("remove/{objectId}")]
        public async Task<IActionResult> RemoveObject(int objectId)
            => Json(await _objectService.RemoveObjectAsync(objectId));  
    }
}