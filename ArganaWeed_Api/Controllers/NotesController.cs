using ArganaWeedApi.Data;
using ArganaWeedApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArganaWeedApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly ArganaWeedDbContext _context;

        public NotesController(ArganaWeedDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetAllNotes()
        {
            var notes = await _context.GetAllNotesAsync();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNoteById(int id)
        {
            var note = await _context.GetNoteByIdAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        [HttpGet("plantule/{plantuleId}")]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotesByPlantuleId(int plantuleId)
        {
            var notes = await _context.GetNotesByPlantuleIdAsync(plantuleId);
            return Ok(notes);
        }

        [HttpGet("date/{noteDate}")]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotesByDate(string noteDate)
        {
            if (DateTime.TryParse(noteDate, out DateTime parsedDate))
            {
                var notes = await _context.GetNotesByDateAsync(parsedDate);
                return Ok(notes);
            }
            return BadRequest("Invalid date format.");
        }

        [HttpGet("username/{userName}")]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotesByUserName(string userName)
        {
            var notes = await _context.GetNotesByUserNameAsync(userName);
            return Ok(notes);
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddNote([FromBody] NoteRequest request)
        {
            var noteDate = request.NoteDate.HasValue ? request.NoteDate.Value : DateTime.Now;
            var noteRappelDate = request.NoteRappelDate;

            var message = await _context.AddNoteAsync(
                request.NoteTexte,
                noteDate,
                noteRappelDate,
                request.PlantuleId,
                User.Identity.Name
            );
            return Ok(message);
        }

        public class NoteRequest
        {
            public string NoteTexte { get; set; }
            public DateTime? NoteDate { get; set; }
            public DateTime? NoteRappelDate { get; set; }
            public int PlantuleId { get; set; }
        }

    }
}
