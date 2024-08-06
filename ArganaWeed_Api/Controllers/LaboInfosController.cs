using ArganaWeedApp.Data;
using ArganaWeedApp.DTOs;
using ArganaWeedApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ArganaWeedApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboInfosController : ControllerBase
    {
        private readonly ArganaWeedDbContext _context;

        public LaboInfosController(ArganaWeedDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<LaboInfosResponse> GetLaboInfo()
        {
            return await _context.GetLaboInfoAsync();
        }

        [HttpPut]
        public async Task<ActionResult<string>> UpdateLaboInfo([FromBody] LaboInfo laboInfo)
        {
            if (laboInfo == null)
            {
                return BadRequest("Invalid laboInfo data.");
            }

            var message = await _context.UpdateLaboInfoAsync(laboInfo);
            return Ok(message);
        }
    }
}
