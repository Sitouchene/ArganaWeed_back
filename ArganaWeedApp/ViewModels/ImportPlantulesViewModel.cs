using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Maui.Controls;
using System.Globalization;
using ArganaWeedApp.Services;
using Microsoft.Maui.Storage;

namespace ArganaWeedApp.ViewModels
{
    public class ImportPlantulesViewModel : BindableObject
    {
        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PlantuleImport> Plantules { get; } = new ObservableCollection<PlantuleImport>();

        public ICommand ChooseFileCommand { get; }
        public ICommand ImportCommand { get; }

        public ImportPlantulesViewModel()
        {
            ChooseFileCommand = new Command(async () => await ChooseFileAsync());
            ImportCommand = new Command(async () => await ImportAsync());
        }

        private async Task ChooseFileAsync()
        {
            try
            {
                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] { "text/csv" } },
                    { DevicePlatform.iOS, new[] { "public.comma-separated-values-text" } },
                    { DevicePlatform.MacCatalyst, new[] { "public.comma-separated-values-text" } },
                    { DevicePlatform.WinUI, new[] { ".csv" } },
                    { DevicePlatform.Tizen, new[] { "text/csv" } }
                });

                var result = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = customFileType,
                    PickerTitle = "Veuillez choisir un fichier CSV"
                });

                if (result == null)
                    return;

                if (!result.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    Message = "Le fichier sélectionné n'est pas un fichier CSV.";
                    return;
                }

                using (var stream = await result.OpenReadAsync())
                using (var reader = new StreamReader(stream))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    var records = csv.GetRecords<PlantuleImport>().ToList();
                    ValidateRecords(records);
                }
            }
            catch (Exception ex)
            {
                Message = $"Erreur lors de l'importation du fichier : {ex.Message}";
            }
        }

        private void ValidateRecords(List<PlantuleImport> records)
        {
            Plantules.Clear();
            var invalidLines = new List<int>();

            for (int i = 0; i < records.Count; i++)
            {
                var record = records[i];
                record.Numero = i + 1;

                if (record.VarieteId <= 0 || record.ProvenanceId <= 0 || record.EmplacementId <= 0 || record.DateReception == default)
                {
                    invalidLines.Add(i + 1);
                }
                else
                {
                    Plantules.Add(record);
                }
            }

            if (invalidLines.Any())
            {
                Message = $"Revoir le format du fichier aux lignes {string.Join(", ", invalidLines)}.";
            }
            else
            {
                Message = $"{records.Count} plantules prêtes à l'importation.";
            }
        }

        private async Task ImportAsync()
        {
            try
            {
                var plantulesList = Plantules.ToList();
                var responseMessage = await ApiService.ImportPlantulesAsync(plantulesList);

                Message = responseMessage;
            }
            catch (Exception ex)
            {
                Message = $"Erreur lors de l'importation des données : {ex.Message}";
            }
        }
    }

    public class PlantuleImport
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
