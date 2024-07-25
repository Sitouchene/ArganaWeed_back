using Microsoft.AspNetCore.Mvc;
using ArganaWeedApi.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArganaWeedApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace ArganaWeedApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VarieteController : ControllerBase
    {
        private readonly ArganaWeedDbContext _context;

        public VarieteController(ArganaWeedDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddVariete(Variete variete)
        {
            var message = await _context.AddVarieteAsync(variete.VarieteCode, variete.VarieteNom, variete.VarieteDescription, variete.VarieteCategorie);
            return Ok(message);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdateVariete(int id, Variete variete)
        {
            var message = await _context.UpdateVarieteAsync(id, variete.VarieteCode, variete.VarieteNom, variete.VarieteDescription, variete.VarieteCategorie);
            return Ok(message);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Variete>>> GetAllVarietes()
        {
            var varietes = await _context.GetAllVarietesAsync();
            return Ok(varietes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Variete>> GetVarieteById(int id)
        {
            var variete = await _context.GetVarieteByIdAsync(id);
            if (variete == null)
            {
                return NotFound();
            }
            return Ok(variete);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteVarieteById(int id)
        {
            var message = await _context.DeleteVarieteByIdAsync(id);
            return Ok(message);
        }

        [HttpGet("search/{searchString}")]
        public async Task<ActionResult<IEnumerable<Variete>>> SearchVariete(string searchString)
        {
            var varietes = await _context.SearchVarieteAsync(searchString);
            return Ok(varietes);
        }
    }
}
