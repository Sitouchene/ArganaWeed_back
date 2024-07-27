using ArganaWeedApp.Data;
using ArganaWeedApp.DTOs;
using ArganaWeedApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArganaWeedApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class EventsController : ControllerBase
    {
        private readonly ArganaWeedDbContext _context;

        public EventsController(ArganaWeedDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<EventsResponse> GetAllEvents()
        {
            return await _context.GetAllEventsAsync();
        }

        [HttpGet("{id}")]
        public async Task<EventsResponse> GetEventById(int id)
        {
            return await _context.GetEventByIdAsync(id);
        }

        [HttpGet("plantule/{plantuleId}")]
        public async Task<EventsResponse> GetEventsByPlantuleId(int plantuleId)
        {
            return await _context.GetEventsByPlantuleIdAsync(plantuleId);
        }

        [HttpGet("date/{eventDate}")]
        public async Task<ActionResult<EventsResponse>> GetEventsByDate(string eventDate)
        {
            if (DateTime.TryParse(eventDate, out DateTime parsedDate))
            {
                var eventsResponse = await _context.GetEventByDateAsync(parsedDate);
                return Ok(eventsResponse);
            }
            return BadRequest("Invalid date format.");
        }

        [HttpGet("username/{userName}")]
        public async Task<EventsResponse> GetEventsByUserName(string userName)
        {
            return await _context.GetEventsByUserNameAsync(userName);
        }

        [HttpGet("type/{eventType}")]
        public async Task<EventsResponse> GetEventsByType(string eventType)
        {
            return await _context.GetEventsByTypeAsync(eventType);
        }

        [HttpGet("nature/{eventNature}")]
        public async Task<EventsResponse> GetEventsByNature(string eventNature)
        {
            return await _context.GetEventsByNatureAsync(eventNature);
        }



        [HttpPost]
        public async Task<ActionResult<string>> AddEvent([FromBody] EventRequest request)
        {
            var message = await _context.AddEventAsync(
                request.EventDateTime,
                request.EventSource,
                request.EventType,
                request.PlantuleId,
                request.EventNature,
                request.EventValeur,
                User.Identity.Name
            );
            return Ok(message);
        }


        public class EventRequest
        {
            public DateTime EventDateTime { get; set; }
            public string EventSource { get; set; }
            public string EventType { get; set; }
            public int PlantuleId { get; set; }
            public string EventNature { get; set; }
            public string EventValeur { get; set; }
        }
    }
}
