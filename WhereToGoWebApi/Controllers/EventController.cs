using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhereToGoWebApi.Common.Extensions;
using WhereToGoWebApi.Models.EventViewModels;
using WhereToGoWebApi.Services.Interfaces;

namespace WhereToGoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet("getAllEvents")]
        public async Task<ActionResult<IEnumerable<EventViewModel>>> GetAllEvents() =>
            Ok(await eventService.GetAllEvents());

        [Authorize]
        [HttpPost("createEvent")]
        public async Task<IActionResult> CreateEvent(EventViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model not valid");

            var userName = User.Claims.GetUserName(ClaimsIdentity.DefaultNameClaimType);

            var result = await eventService.CreateEvent(model, userName);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            return NoContent();
        }
    }
}