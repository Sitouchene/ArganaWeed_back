using Microsoft.AspNetCore.Mvc;
using ArganaWeedApp.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArganaWeedApp.Models;
using Microsoft.AspNetCore.Authorization;
using ArganaWeedApp.DTOs;

namespace ArganaWeedApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class VarietesController : ControllerBase
    {
        private readonly ArganaWeedDbContext _context;

        public VarietesController(ArganaWeedDbContext context)
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

       [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteVarieteById(int id)
        {
            var message = await _context.DeleteVarieteByIdAsync(id);
            return Ok(message);
        }


        //******* GET

        [HttpGet]
        public async Task<VarietesResponse> GetAllVarietes()
        {
            return await _context.GetAllVarietesAsync();
        }

        [HttpGet("{id}")]
        public async Task<VarietesResponse> GetVarieteById(int id)
        {
            return await _context.GetVarieteByIdAsync(id);
        }

        [HttpGet("search/{searchString}")]
        public async Task<VarietesResponse> SearchVariete(string searchString)
        {
            return await _context.SearchVarieteAsync(searchString);
        }



    }
}
