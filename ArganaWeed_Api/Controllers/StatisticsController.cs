using ArganaWeed_Api.Data;
using ArganaWeed_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArganaWeed_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly ArganaWeedDbContext _context;

        public StatisticsController(ArganaWeedDbContext context)
        {
            _context = context;
        }

        [HttpGet("plantules-stats")]
        public async Task<ActionResult<IEnumerable<PlantulesStats>>> GetPlantulesStats()
        {
            var stats = await _context.PlantulesStats.ToListAsync();
            return Ok(stats);
        }

        [HttpGet("plantules-par-categorie")]
        public async Task<ActionResult<IEnumerable<PlantulesParCategorie>>> GetPlantulesParCategorie()
        {
            var categories = await _context.PlantulesParCategorie.ToListAsync();
            return Ok(categories);
        }

        [HttpGet("plantules-par-stade")]
        public async Task<ActionResult<IEnumerable<PlantulesParStade>>> GetPlantulesParStade()
        {
            var stades = await _context.PlantulesParStade.ToListAsync();
            return Ok(stades);
        }

        [HttpGet("plantules-par-sante")]
        public async Task<ActionResult<IEnumerable<PlantulesParSante>>> GetPlantulesParSante()
        {
            var santes = await _context.PlantulesParSante.ToListAsync();
            return Ok(santes);
        }

        [HttpGet("evolution-mensuelle")]
        public async Task<ActionResult<IEnumerable<EvolutionMensuellePlantules>>> GetEvolutionMensuelle()
        {
            var evolutions = await _context.EvolutionMensuellePlantules.ToListAsync();
            return Ok(evolutions);
        }
    }
}
