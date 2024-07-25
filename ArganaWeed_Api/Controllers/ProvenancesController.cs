using Microsoft.AspNetCore.Mvc;
using ArganaWeedApi.Data;
using ArganaWeedApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArganaWeedApi.Data;
using Microsoft.AspNetCore.Authorization;

namespace ArganaWeedApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProvenanceController : ControllerBase
    {
        private readonly ArganaWeedDbContext _context;

        public ProvenanceController(ArganaWeedDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddProvenance(Provenance provenance)
        {
            var message = await _context.AddProvenanceAsync(provenance.ProvenanceNom, provenance.ProvenanceDescription);
            return Ok(message);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdateProvenance(int id, Provenance provenance)
        {
            var message = await _context.UpdateProvenanceAsync(id, provenance.ProvenanceNom, provenance.ProvenanceDescription);
            return Ok(message);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provenance>>> GetAllProvenances()
        {
            var provenances = await _context.GetAllProvenancesAsync();
            return Ok(provenances);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Provenance>> GetProvenanceById(int id)
        {
            var provenance = await _context.GetProvenanceByIdAsync(id);
            if (provenance == null)
            {
                return NotFound();
            }
            return Ok(provenance);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteProvenanceById(int id)
        {
            var message = await _context.DeleteProvenanceByIdAsync(id);
            return Ok(message);
        }

        [HttpGet("search/{searchString}")]
        public async Task<ActionResult<IEnumerable<Provenance>>> SearchProvenance(string searchString)
        {
            var provenances = await _context.SearchProvenanceAsync(searchString);
            return Ok(provenances);
        }
    }
}
