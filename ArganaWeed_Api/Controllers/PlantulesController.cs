﻿using ArganaWeedApp.Data;
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
    
    public class PlantulesController : ControllerBase
    {
        private readonly ArganaWeedDbContext _context;

        public PlantulesController(ArganaWeedDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<PlantulesDetailResponse> GetAllPlantules()
        {
            return await _context.GetAllPlantulesAsync();
        }

        [HttpGet("{id}")]
        public async Task<PlantulesDetailResponse> GetPlantuleById(int id)
        {
            return await _context.GetPlantuleByIdAsync(id);
        }

        [HttpGet("search/{searchString}")]
        public async Task<ActionResult<PlantulesDetailResponse>> SearchPlantules(string searchString)
        {
            var plantulesResponse = await _context.SearchPlantulesAsync(searchString);
            return Ok(plantulesResponse);
        }

        [HttpGet("slug/{slug}")]
        public async Task<PlantulesDetailResponse> GetPlantuleBySlug(string slug)
        {
            return await _context.GetPlantuleBySlugAsync(slug);
        }

        [HttpGet("variete/{varieteCode}")]
        public async Task<PlantulesDetailResponse> GetPlantuleByVariete(string varieteCode)
        {
            return await _context.GetPlantuleByVarieteAsync(varieteCode);
        }

        [HttpGet("active")]
        public async Task<PlantulesDetailResponse> GetPlantulesActive()
        {
            return await _context.GetPlantulesActiveAsync();
        }

        [HttpGet("inactive")]
        public async Task<PlantulesDetailResponse> GetPlantulesInactive()
        {
            return await _context.GetPlantulesInactiveAsync();
        }

        [HttpGet("archived")]
        public async Task<PlantulesDetailResponse> GetPlantulesArchived()
        {
            return await _context.GetPlantulesArchivedAsync();
        }

        [HttpGet("latest")]
        public async Task<ActionResult<int>> GetLatestPlantuleId()
        {
            var latestPlantuleId = await _context.GetLatestPlantuleIdAsync();
            return Ok(latestPlantuleId);
        }


        /*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantuleDetail>>> GetAllPlantules()
        {
            var plantules = await _context.GetAllPlantulesAsync();
            return Ok(plantules);
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<PlantuleDetail>>> GetPlantulesActive()
        {
            var plantules = await _context.GetPlantulesActiveAsync();
            return Ok(plantules);
        }

        [HttpGet("inactive")]
        public async Task<ActionResult<IEnumerable<PlantuleDetail>>> GetPlantulesInactive()
        {
            var plantules = await _context.GetPlantulesInactiveAsync();
            return Ok(plantules);
        }
        [HttpGet("archived")]
        public async Task<ActionResult<IEnumerable<PlantuleDetail>>> GetPlantulesArchived()
        {
            var plantules = await _context.GetPlantulesArchivedAsync();
            return Ok(plantules);
        }

        [HttpGet("search/{searchString}")]
        public async Task<ActionResult<IEnumerable<PlantuleDetail>>> SearchPlantules(string searchString)
        {
            var plantules = await _context.SearchPlantulesAsync(searchString);
            return Ok(plantules);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlantuleDetail>> GetPlantule(int id)
        {
            var plantule = await _context.GetPlantuleByIdAsync(id);
            if (plantule == null)
            {
                return NotFound();
            }
            return Ok(plantule);
        }

        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<PlantuleDetail>> GetPlantuleBySlug(string slug)
        {
            var plantule = await _context.GetPlantuleBySlugAsync(slug);
            if (plantule == null)
            {
                return NotFound();
            }
            return Ok(plantule);
        }

        [HttpGet("variete/{varieteCode}")]
        public async Task<ActionResult<IEnumerable<PlantuleDetail>>> GetPlantuleByVariete(string varieteCode)
        {
            var plantules = await _context.GetPlantuleByVarieteAsync(varieteCode);
            return Ok(plantules);
        }
        */
        [HttpPost]
        public async Task<ActionResult<string>> AddPlantule([FromBody] PlantuleAddRequest request)
        {
            var message = await _context.AddPlantuleAsync(
                request.VarieteId,
                request.PlantuleDescription,
                request.DateReception,
                request.ProvenanceId,
                request.Stade,
                request.Sante,
                request.EmplacementId,
                User.Identity.Name
            );
            return Ok(message);
        }

        [HttpPost("import")]
        public async Task<ActionResult<string>> ImportPlantules([FromBody] List<PlantuleImportRequest> plantules)
        {
            if (plantules == null || !plantules.Any())
            {
                return BadRequest("La liste des plantules est vide ou invalide.");
            }

            var importResults = new List<string>();

            foreach (var plantule in plantules)
            {
                var result = await _context.AddPlantuleAsync(
                    plantule.VarieteId,
                    plantule.PlantuleDescription,
                    plantule.DateReception,
                    plantule.ProvenanceId,
                    plantule.Stade,
                    plantule.Sante,
                    plantule.EmplacementId,
                    "System" // Vous pouvez remplacer "System" par le nom de l'utilisateur approprié
                );

                importResults.Add($"Ligne {plantule.Numero}: {result}");
            }

            return Ok(new { Message = "Importation terminée avec succès!", Details = importResults });
        }

       




        [HttpPut("emplacement/{id}")]
        public async Task<ActionResult<string>> UpdatePlantuleEmplacement(int id, [FromBody] UpdateEmplacementRequest request)
        {
            var message = await _context.UpdatePlantuleEmplacementAsync(id, request.EmplacementId, User.Identity.Name);
            return Ok(message);
        }

        public class UpdateEmplacementRequest
        {
            public int EmplacementId { get; set; }
        }


        [HttpPut("stade/{id}")]
        public async Task<ActionResult<string>> UpdatePlantuleStade(int id, [FromBody] UpdateStadeRequest request)
        {
            var message = await _context.UpdatePlantuleStadeAsync(id, request.Stade, User.Identity.Name);
            return Ok(message);
        }

        [HttpPut("sortie/{id}")]
        public async Task<ActionResult<string>> UpdatePlantuleSortie(int id, [FromBody] UpdateSortieRequest request)
        {
            var message = await _context.UpdatePlantuleSortieAsync(id, request.SortieDate, request.SortieType, request.SortieObservation, User.Identity.Name);
            return Ok(message);
        }

        [HttpPut("sante/{id}")]
        public async Task<ActionResult<string>> UpdatePlantuleSante(int id, [FromBody] UpdateSanteRequest request)
        {
            var message = await _context.UpdatePlantuleSanteAsync(id, request.Sante, User.Identity.Name);
            return Ok(message);
        }

        public class UpdateSanteRequest
        {
            public string Sante { get; set; }
        }

        [HttpPut("archive")]
        public async Task<ActionResult<string>> ArchivePlantules([FromBody] ArchivePlantulesRequest request)
        {
            var message = await _context.ArchivePlantulesAsync(request.EndDate, User.Identity.Name);
            return Ok(message);
        }
        public class ArchivePlantulesRequest
        {
            public DateTime EndDate { get; set; }
        }

        public class UpdateStadeRequest
        {
            public string Stade { get; set; }
        }

        public class UpdateSortieRequest
        {
            public DateTime? SortieDate { get; set; }
            public string SortieType { get; set; }
            public string SortieObservation { get; set; }
        }
        public class PlantuleAddRequest
        {
            public int VarieteId { get; set; }
            public string PlantuleDescription { get; set; }
            public DateTime DateReception { get; set; }
            public int ProvenanceId { get; set; }
            public string Stade { get; set; }
            public string Sante { get; set; }
            public int EmplacementId { get; set; }
        }

        public class PlantuleImportRequest
        {
            public int Numero { get; set; }
            public int VarieteId { get; set; }
            public string PlantuleDescription { get; set; }
            public DateTime DateReception { get; set; }
            public int ProvenanceId { get; set; }
            public string Stade { get; set; }
            public string Sante { get; set; }
            public int EmplacementId { get; set; }
        }
    }
}
