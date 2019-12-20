﻿using System.Collections.Generic;
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
        public async Task<ActionResult<EventViewModel>> GetEvent(int eventId) =>
            Ok(await eventService.GetEvent(eventId));

        [HttpGet("getAllEvents")]
        public async Task<ActionResult<IEnumerable<EventViewModel>>> GetAllEvents() =>
            Ok(await eventService.GetAllEvents());

        [HttpGet("getEventsByFilters")]
        public async Task<ActionResult<IEnumerable<EventViewModel>>> GetEventsByFilters(EventViewModelFilter filters)
        {
            var events = await eventService.GetEventsByFilters(filters);

            return Ok(events);
        }

        [Authorize(Roles = AppRoles.organaizerRole)]
        [HttpPost("createEvent")]   
        public async Task<IActionResult> CreateEvent(EventViewModel model)
        {
            var userId = User.Claims.GetUserClaim(AppClaims.IdClaim);

            var result = await eventService.CreateEvent(model, userId);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            return NoContent();
        }

    }
}