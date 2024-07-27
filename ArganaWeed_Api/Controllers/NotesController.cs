using ArganaWeedApp.Data;
using ArganaWeedApp.DTOs;
using ArganaWeedApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArganaWeedApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class NotesController : ControllerBase
    {
        private readonly ArganaWeedDbContext _context;

        public NotesController(ArganaWeedDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<NotesResponse> GetAllNotes()
        {
            return await _context.GetAllNotesAsync();
        }

        [HttpGet("{id}")]
        public async Task<NotesResponse> GetNoteById(int id)
        {
            return await _context.GetNoteByIdAsync(id);
        }

        [HttpGet("plantule/{plantuleId}")]
        public async Task<NotesResponse> GetNotesByPlantuleId(int plantuleId)
        {
            return await _context.GetNotesByPlantuleIdAsync(plantuleId);
        }

        [HttpGet("date/{noteDate}")]
        public async Task<ActionResult<NotesResponse>> GetNotesByDate(string noteDate)
        {
            if (DateTime.TryParse(noteDate, out DateTime parsedDate))
            {
                var notesResponse = await _context.GetNotesByDateAsync(parsedDate);
                return Ok(notesResponse);
            }
            return BadRequest("Invalid date format.");
        }

        [HttpGet("username/{userName}")]
        public async Task<NotesResponse> GetNotesByUserName(string userName)
        {
            return await _context.GetNotesByUserNameAsync(userName);
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
