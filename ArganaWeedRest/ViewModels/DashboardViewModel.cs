using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArganaWeedRest.Models;
using ArganaWeedRest.Services;

namespace ArganaWeedRest.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private readonly IStatisticsService _statisticsService;

        public ObservableCollection<PlantulesStats> PlantulesStats { get; private set; }
        public ObservableCollection<PlantulesParCategorie> PlantulesParCategorie { get; private set; }
        public ObservableCollection<PlantulesParStade> PlantulesParStade { get; private set; }
        public ObservableCollection<PlantulesParSante> PlantulesParSante { get; private set; }
        public ObservableCollection<EvolutionMensuellePlantules> EvolutionMensuelle { get; private set; }


        //public DashboardViewModel()
        //{
        //}

        public DashboardViewModel(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
            PlantulesStats = new ObservableCollection<PlantulesStats>();
            PlantulesParCategorie = new ObservableCollection<PlantulesParCategorie>();
            PlantulesParStade = new ObservableCollection<PlantulesParStade>();
            PlantulesParSante = new ObservableCollection<PlantulesParSante>();
            EvolutionMensuelle = new ObservableCollection<EvolutionMensuellePlantules>();

            LoadData();
        }


        private async void LoadData()
        {
            try
            {
                var stats = await _statisticsService.GetPlantulesStats();
                var categories = await _statisticsService.GetPlantulesParCategorie();
                var stades = await _statisticsService.GetPlantulesParStade();
                var santes = await _statisticsService.GetPlantulesParSante();
                var evolutions = await _statisticsService.GetEvolutionMensuelle();

                PlantulesStats.Clear();
                PlantulesParCategorie.Clear();
                PlantulesParStade.Clear();
                PlantulesParSante.Clear();
                EvolutionMensuelle.Clear();

                foreach (var stat in stats)
                    PlantulesStats.Add(stat);
                foreach (var categorie in categories)
                    PlantulesParCategorie.Add(categorie);
                foreach (var stade in stades)
                    PlantulesParStade.Add(stade);
                foreach (var sante in santes)
                    PlantulesParSante.Add(sante);
                foreach (var evolution in evolutions)
                    EvolutionMensuelle.Add(evolution);
            }
            catch (Exception ex)
            {
                // Gestion des erreurs (afficher un message, journaliser l'erreur, etc.)
                Console.WriteLine($"Erreur lors du chargement des données : {ex.Message}");
            }
        }
    }
}
