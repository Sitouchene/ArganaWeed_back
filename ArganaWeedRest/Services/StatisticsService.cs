using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ArganaWeedRest.Models;
using Newtonsoft.Json;

namespace ArganaWeedRest.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly HttpClient _httpClient;

        public StatisticsService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ArganaWeedApiClient");
        }

        public async Task<IEnumerable<PlantulesStats>> GetPlantulesStats()
        {
            var response = await _httpClient.GetStringAsync("statistics/plantules-stats");
            return JsonConvert.DeserializeObject<IEnumerable<PlantulesStats>>(response);
        }

        public async Task<IEnumerable<PlantulesParCategorie>> GetPlantulesParCategorie()
        {
            var response = await _httpClient.GetStringAsync("statistics/plantules-par-categorie");
            return JsonConvert.DeserializeObject<IEnumerable<PlantulesParCategorie>>(response);
        }

        public async Task<IEnumerable<PlantulesParStade>> GetPlantulesParStade()
        {
            var response = await _httpClient.GetStringAsync("statistics/plantules-par-stade");
            return JsonConvert.DeserializeObject<IEnumerable<PlantulesParStade>>(response);
        }

        public async Task<IEnumerable<PlantulesParSante>> GetPlantulesParSante()
        {
            var response = await _httpClient.GetStringAsync("statistics/plantules-par-sante");
            return JsonConvert.DeserializeObject<IEnumerable<PlantulesParSante>>(response);
        }

        public async Task<IEnumerable<EvolutionMensuellePlantules>> GetEvolutionMensuelle()
        {
            var response = await _httpClient.GetStringAsync("statistics/evolution-mensuelle");
            return JsonConvert.DeserializeObject<IEnumerable<EvolutionMensuellePlantules>>(response);
        }
    }
}
