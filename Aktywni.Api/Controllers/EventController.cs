using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands;
using Aktywni.Infrastructure.Commands.Event;
using Aktywni.Infrastructure.Commands.Object;
using Aktywni.Infrastructure.DTO;
using Aktywni.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Aktywni.Api.Controllers
{
    public class EventController : ApiControllerBase
    {
        private IEventService _eventService;
        private readonly IConfiguration _configuration;
        public EventController(IEventService eventService, IConfiguration configuration)
        {
            _eventService = eventService;
            _configuration = configuration;
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEvent(int eventId)
            => Json(await _eventService.GetEventAsync(eventId));

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetEvent(string name)
            => Json(await _eventService.GetEventAsync(name));

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
            => Json(await _eventService.GetAllEventsAsync());

        [HttpGet("my")]
        public async Task<IActionResult> GetAllMyEvents()
            => Json(await _eventService.GetAllMyEventsAsync(UserId));

        [HttpPost("search")]
        public async Task<IActionResult> SearchEvents([FromBody]SearchObject command)
            => Json(await _eventService.SearchEventsAsync(command.Name));

        [HttpPost("searchInDiscipline")]
        public async Task<IActionResult> SearchEventsInDiscipline([FromBody]SearchEventsInDiscipline command)
            => Json(await _eventService.SearchEventsInDisciplineAsync(command.Name, command.DisciplineId));

        [HttpPost("searchInDisciplineAndDistance")]
        public async Task<IActionResult> SearchEventsInDisciplineAndDistance([FromBody]SearchEventsInDisciplineAndDistance command)
            => Json(await _eventService.SearchEventsInDisciplineAndDistanceAsync(command.Name, command.DisciplineId, command.Distance));

        [HttpPost("add")]
        public async Task<IActionResult> AddEvent([FromBody]AddEvent command)
           => Json(await _eventService.AddEventAsync(command.Name, command.Date, UserId, command.DisciplineId, command.Description, command.GeographicalCoordinates));

        [HttpPut("changeName/{eventId}")]
        public async Task<IActionResult> ChangeEventName(int eventId, [FromBody]ChangeNameEvent command)
            => Json(await _eventService.ChangeNameEventAsync(eventId, command.NewName));

        [HttpPut("changeVisibility/{eventId}")]
        public async Task<IActionResult> ChangeVisibilityEvent(int eventId, [FromBody]ChangeVisibilityEvent command)
            => Json(await _eventService.ChangeVisibilityEventAsync(eventId, command.Visibility));

        [HttpDelete("remove/{eventId}")]
        public async Task<IActionResult> RemoveEvent(int eventId)
            => Json(await _eventService.RemoveEventAsync(eventId));

        [HttpPut("changeDescritpion/{eventId}")]
        public async Task<IActionResult> ChangeDescriptionEvent(int eventId, [FromBody]ChangeDescriptionEvent command)
            => Json(await _eventService.ChangeDescription(eventId, command.NewDescription));

        [HttpPut("changeDate/{eventId}")]
         public async Task<IActionResult> ChangeDateEvent(int eventId, [FromBody]ChangeDateEvent command)
            => Json(await _eventService.ChangeDateEventAsync(eventId, Convert.ToDateTime(command.NewDate)));

        [HttpPut("changeGeographicalCoordinates/{eventId}")]
        public async Task<IActionResult> ChangeGeographicalCoordinatesEvent(int eventId, [FromBody]ChangeGeographicalCoordinatesEvent command)
            => Json(await _eventService.ChangeGeographicalCoordinatesEventAsync(eventId, command.NewGeographicalCoordinates));

    }
}