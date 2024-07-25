using ArganaWeedApi.Data;
using ArganaWeedApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArganaWeedApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmplacementsController : ControllerBase
    {
        private readonly ArganaWeedDbContext _context;

        public EmplacementsController(ArganaWeedDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Owner, Viewer, Agent")]
        public async Task<ActionResult<IEnumerable<Emplacement>>> GetAllEmplacements()
        {
            var emplacements = await _context.GetAllEmplacementsAsync();
            return Ok(emplacements);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Owner, Viewer, Agent")]
        public async Task<ActionResult<Emplacement>> GetEmplacementById(int id)
        {
            var emplacement = await _context.GetEmplacementByIdAsync(id);
            if (emplacement == null)
            {
                return NotFound();
            }
            return Ok(emplacement);
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<string>> AddEmplacement([FromBody] Emplacement emplacement)
        {
            if (emplacement == null)
            {
                return BadRequest("Emplacement data is null.");
            }

            var message = await _context.AddEmplacementAsync(emplacement.EmplacementCode, emplacement.EmplacementDescription);
            return Ok(message);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<string>> UpdateEmplacement(int id, [FromBody] Emplacement emplacement)
        {
            if (emplacement == null)
            {
                return BadRequest("Invalid emplacement data.");
            }

            var message = await _context.UpdateEmplacementAsync(id, emplacement.EmplacementCode, emplacement.EmplacementDescription);
            return Ok(message);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<string>> DeleteEmplacement(int id)
        {
            var message = await _context.DeleteEmplacementByIdAsync(id);
            return Ok(message);
        }

        [HttpGet("search/{searchString}")]
        [Authorize(Roles = "Owner, Viewer, Agent")]
        public async Task<ActionResult<IEnumerable<Emplacement>>> SearchEmplacement(string searchString)
        {
            var emplacements = await _context.SearchEmplacementAsync(searchString);
            return Ok(emplacements);
        }


    }
}
