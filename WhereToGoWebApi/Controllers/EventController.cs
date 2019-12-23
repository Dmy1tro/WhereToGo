using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhereToGoWebApi.Common.Authentication;
using WhereToGoWebApi.Common.Extensions;
using WhereToGoWebApi.Models.EventViewModels;
using WhereToGoWebApi.Services.Interfaces;

namespace WhereToGoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet("{eventId}")]
        public async Task<ActionResult<EventFullViewModel>> GetEvent(int eventId)
        {
            var result = await eventService.GetEvent(eventId);

            return result != null
                ? Ok(result) as ActionResult
                : BadRequest($"Event with id '{eventId}' not found");
        }

        [HttpGet("getImageForEvent/{eventId}")]
        public async Task<IActionResult> GetImageForEvent(int eventId)
        {
            var result = await eventService.GetImageOfEvent(eventId);

            return result ?? new NotFoundResult() as ActionResult;
        }

        [HttpGet("getAllEvents")]
        public async Task<ActionResult<IEnumerable<EventViewModel>>> GetAllEvents() =>
            Ok(await eventService.GetAllEvents());

        [HttpGet("getEventsByFilters")]
        public async Task<ActionResult<IEnumerable<EventViewModel>>> GetEventsByFilters([FromQuery] EventViewModelFilter filters)
        {
            var events = await eventService.GetEventsByFilters(filters);

            return Ok(events);
        }

        [Authorize(Roles = AppRoles.organaizerRole)]
        [HttpPost("createEvent")]   
        public async Task<IActionResult> CreateEvent([FromForm] EventViewModel model)
        {
            var userId = User.Claims.GetUserClaim(AppClaims.IdClaim);

            var result = await eventService.CreateEvent(model, userId);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            return NoContent();
        }

    }
}