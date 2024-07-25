using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArganaWeedRest.Models;

namespace ArganaWeedRest.Services
{
    public interface IStatisticsService
    {
        Task<IEnumerable<PlantulesStats>> GetPlantulesStats();
        Task<IEnumerable<PlantulesParCategorie>> GetPlantulesParCategorie();
        Task<IEnumerable<PlantulesParStade>> GetPlantulesParStade();
        Task<IEnumerable<PlantulesParSante>> GetPlantulesParSante();
        Task<IEnumerable<EvolutionMensuellePlantules>> GetEvolutionMensuelle();
    }
}
