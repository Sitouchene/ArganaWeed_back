using ArganaWeed_Api.Data;
using ArganaWeed_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArganaWeed_Api.Controllers
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

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEventById(int id)
        {
            var @event = await _context.GetEventByIdAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return Ok(@event);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEvents()
        {
            var events = await _context.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("plantule/{plantuleId}")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsByPlantuleId(int plantuleId)
        {
            var events = await _context.GetEventsByPlantuleIdAsync(plantuleId);
            return Ok(events);
        }

        [HttpGet("date/{eventDate}")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsByDate(string eventDate)
        {
            if (DateTime.TryParse(eventDate, out DateTime parsedDate))
            {
                var events = await _context.GetEventByDateAsync(parsedDate);
                return Ok(events);
            }
            return BadRequest("Invalid date format.");
        }

        [HttpGet("username/{userName}")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsByUserName(string userName)
        {
            var events = await _context.GetEventsByUserNameAsync(userName);
            return Ok(events);
        }

        [HttpGet("type/{eventType}")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsByType(string eventType)
        {
            var events = await _context.GetEventsByTypeAsync(eventType);
            return Ok(events);
        }

        [HttpGet("nature/{eventNature}")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsByNature(string eventNature)
        {
            var events = await _context.GetEventsByNatureAsync(eventNature);
            return Ok(events);
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
