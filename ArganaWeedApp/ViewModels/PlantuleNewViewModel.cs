using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ArganaWeedApp.Models;
using ArganaWeedApp.Services;
using Microsoft.Maui.Controls;
using ArganaWeedApp.DTOs;

namespace ArganaWeedApp.ViewModels
{
    public class PlantuleNewViewModel : BindableObject
    {
        private string _errorMessage;
        private string _plantuleDescription;
        private DateTime _dateReception;
        private string _sante;
        private string _stade;
        private Variete _selectedVariete;
        private Provenance _selectedProvenance;
        private Emplacement _selectedEmplacement;
        private bool _isPlantuleCreated;

        public PlantuleNewViewModel()
        {
            Varietes = new ObservableCollection<Variete>();
            Provenances = new ObservableCollection<Provenance>();
            Emplacements = new ObservableCollection<Emplacement>();
            SanteOptions = new ObservableCollection<string> { "Bon", "Moyen", "Mauvais", "En danger" };
            StadeOptions = new ObservableCollection<string> { "Initiation", "Micro dissection", "Magenta", "Double magenta", "Hydroponie" };
            DateReception = DateTime.Now;

            LoadDataCommand = new Command(async () => await LoadDataAsync());
            SavePlantuleCommand = new Command(async () => await SavePlantuleAsync());

            LoadDataCommand.Execute(null);
        }

        public ObservableCollection<Variete> Varietes { get; }
        public ObservableCollection<Provenance> Provenances { get; }
        public ObservableCollection<Emplacement> Emplacements { get; }
        public ObservableCollection<string> SanteOptions { get; }
        public ObservableCollection<string> StadeOptions { get; }
        public PlantuleDetail CreatedPlantule { get; private set; }

        public string PlantuleDescription
        {
            get => _plantuleDescription;
            set
            {
                _plantuleDescription = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateReception
        {
            get => _dateReception;
            set
            {
                _dateReception = value;
                OnPropertyChanged();
            }
        }

        public string Sante
        {
            get => _sante;
            set
            {
                _sante = value;
                OnPropertyChanged();
            }
        }

        public string Stade
        {
            get => _stade;
            set
            {
                _stade = value;
                OnPropertyChanged();
            }
        }

        public Variete SelectedVariete
        {
            get => _selectedVariete;
            set
            {
                _selectedVariete = value;
                OnPropertyChanged();
            }
        }

        public Provenance SelectedProvenance
        {
            get => _selectedProvenance;
            set
            {
                _selectedProvenance = value;
                OnPropertyChanged();
            }
        }

        public Emplacement SelectedEmplacement
        {
            get => _selectedEmplacement;
            set
            {
                _selectedEmplacement = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsPlantuleCreated
        {
            get => _isPlantuleCreated;
            set
            {
                _isPlantuleCreated = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadDataCommand { get; }
        public ICommand SavePlantuleCommand { get; }

        private async Task LoadDataAsync()
        {
            try
            {
                var varietes = await ApiService.GetVarietesAsync();
                var provenances = await ApiService.GetProvenancesAsync();
                var emplacements = await ApiService.GetEmplacementsAsync();

                Varietes.Clear();
                foreach (var variete in varietes)
                {
                    Varietes.Add(variete);
                }

                Provenances.Clear();
                foreach (var provenance in provenances)
                {
                    Provenances.Add(provenance);
                }

                Emplacements.Clear();
                foreach (var emplacement in emplacements)
                {
                    Emplacements.Add(emplacement);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Erreur lors du chargement des données : {ex.Message}";
            }
        }

        public async Task SavePlantuleAsync()
        {
            if (SelectedVariete == null || SelectedProvenance == null || SelectedEmplacement == null || DateReception == default)
            {
                ErrorMessage = "Veuillez remplir tous les champs obligatoires.";
                return;
            }

            var request = new PlantuleAddRequest
            {
                VarieteId = SelectedVariete.VarieteId,
                PlantuleDescription = PlantuleDescription,
                DateReception = DateReception,
                ProvenanceId = SelectedProvenance.ProvenanceId,
                Sante = Sante,
                Stade = Stade,
                EmplacementId = SelectedEmplacement.EmplacementId
            };

            try
            {
                await ApiService.AddPlantuleAsync(request);
                var lastPlantuleId = await ApiService.GetLatestPlantuleIdAsync();
                CreatedPlantule = await ApiService.GetPlantuleByIdAsync(lastPlantuleId);

                if (CreatedPlantule != null)
                {
                    ErrorMessage = "Plantule ajoutée avec succès.";
                    IsPlantuleCreated = true;
                }
                else
                {
                    ErrorMessage = "Erreur lors de l'ajout de la plantule.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Erreur: {ex.Message}";
            }
        }


        public void Cancel()
        {
            SelectedVariete = null;
            PlantuleDescription = string.Empty;
            DateReception = DateTime.Now;
            SelectedProvenance = null;
            Sante = SanteOptions.First();
            Stade = StadeOptions.First();
            SelectedEmplacement = null;
            ErrorMessage = string.Empty;
        }
    }
}
