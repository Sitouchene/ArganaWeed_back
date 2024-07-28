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
   
    public class EmplacementsController : ControllerBase
    {
        private readonly ArganaWeedDbContext _context;

        public EmplacementsController(ArganaWeedDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<EmplacementsResponse> GetAllEmplacements()
        {
            return await _context.GetAllEmplacementsAsync();
        }

        [HttpGet("{id}")]
        public async Task<EmplacementsResponse> GetEmplacementById(int id)
        {
            return await _context.GetEmplacementByIdAsync(id);
        }



        [HttpGet("search/{searchString}")]
        public async Task<ActionResult<EmplacementsResponse>> SearchEmplacement(string searchString)
        {
            var emplacementsResponse = await _context.SearchEmplacementAsync(searchString);
            return Ok(emplacementsResponse);
        }

       
        [HttpPost]
        //[Authorize(Roles = "Owner")]
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
        //[Authorize(Roles = "Owner")]
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
        //[Authorize(Roles = "Owner")]
        public async Task<ActionResult<string>> DeleteEmplacement(int id)
        {
            var message = await _context.DeleteEmplacementByIdAsync(id);
            return Ok(message);
        }




    }
}
