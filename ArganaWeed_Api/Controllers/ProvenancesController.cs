using Microsoft.AspNetCore.Mvc;
using ArganaWeedApp.Data;
using ArganaWeedApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArganaWeedApp.Data;
using Microsoft.AspNetCore.Authorization;
using ArganaWeedApp.DTOs;

namespace ArganaWeedApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProvenanceController : ControllerBase
    {
        private readonly ArganaWeedDbContext _context;

        public ProvenanceController(ArganaWeedDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ProvenancesResponse> GetAllProvenances()
        {
            return await _context.GetAllProvenancesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ProvenancesResponse> GetProvenanceById(int id)
        {
            return await _context.GetProvenanceByIdAsync(id);
        }

        [HttpGet("search/{searchString}")]
        public async Task<ActionResult<ProvenancesResponse>> SearchProvenance(string searchString)
        {
            var provenancesResponse = await _context.SearchProvenanceAsync(searchString);
            return Ok(provenancesResponse);
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


        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteProvenanceById(int id)
        {
            var message = await _context.DeleteProvenanceByIdAsync(id);
            return Ok(message);
        }


    }
}
