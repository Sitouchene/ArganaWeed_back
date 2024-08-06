using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using ArganaWeedApp.Models;
using ArganaWeedApp.Services;
using Microsoft.Maui.Controls;
using ArganaWeedApp.DTOs;

namespace ArganaWeedApp.ViewModels
{
    public class PlantuleFicheViewModel : BindableObject
    {
        private readonly QRCodeService _qrCodeService;
        private readonly PdfService _pdfService;
        private readonly AlertService _alertService;
        private PlantuleDetail _plantule;
        private ImageSource _qrImage;

        public ObservableCollection<string> SanteOptions { get; }
        public ObservableCollection<string> StadeOptions { get; }
        public ObservableCollection<Emplacement> Emplacements { get; }
        public ObservableCollection<string> SortieTypeOptions { get; }

        private string _selectedSante;
        public string SelectedSante
        {
            get => _selectedSante;
            set
            {
                _selectedSante = value;
                OnPropertyChanged();
            }
        }

        private string _selectedStade;
        public string SelectedStade
        {
            get => _selectedStade;
            set
            {
                _selectedStade = value;
                OnPropertyChanged();
            }
        }

        private Emplacement _selectedEmplacement;
        public Emplacement SelectedEmplacement
        {
            get => _selectedEmplacement;
            set
            {
                _selectedEmplacement = value;
                OnPropertyChanged();
            }
        }

        private DateTime _sortieDate;
        public DateTime SortieDate
        {
            get => _sortieDate;
            set
            {
                _sortieDate = value;
                OnPropertyChanged();
            }
        }

        private string _sortieType;
        public string SortieType
        {
            get => _sortieType;
            set
            {
                _sortieType = value;
                OnPropertyChanged();
            }
        }

        private string _sortieObservation;
        public string SortieObservation
        {
            get => _sortieObservation;
            set
            {
                _sortieObservation = value;
                OnPropertyChanged();
            }
        }

        private string _noteText;
        public string NoteText
        {
            get => _noteText;
            set
            {
                _noteText = value;
                OnPropertyChanged();
            }
        }

        private DateTime _noteRappelDate;
        public DateTime NoteRappelDate
        {
            get => _noteRappelDate;
            set
            {
                _noteRappelDate = value;
                OnPropertyChanged();
            }
        }


        public PlantuleDetail Plantule
        {
            get => _plantule;
            set
            {
                _plantule = value;
                OnPropertyChanged();
                if (LoadEventsCommand != null && LoadNotesCommand != null)
                {
                    LoadEventsCommand.Execute(null); // Charger les événements lorsque la plantule est définie
                    LoadNotesCommand.Execute(null);  // Charger les notes lorsque la plantule est définie
                }
                GenerateQrImage(); // Générer l'image QR lorsque la plantule est définie
                LoadEmplacementsCommand.Execute(null); // Charger les emplacements lorsque la plantule est définie
            }
        }

        public ImageSource QrImage
        {
            get => _qrImage;
            set
            {
                _qrImage = value;
                OnPropertyChanged();
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Event> Events { get; } = new ObservableCollection<Event>();
        public ObservableCollection<Note> Notes { get; } = new ObservableCollection<Note>();

        public ICommand LoadEventsCommand { get; }
        public ICommand LoadNotesCommand { get; }
        public ICommand NavigateBackCommand { get; }
        public ICommand GeneratePdfCommand { get; }
        public ICommand SaveCommand { get; }   //To rename SaveEditPlantuleCommand
        public ICommand CancelCommand { get; }  //To rename CancelEditPlantuleCommand
        public ICommand SortirPlantuleCommand { get; }
        public ICommand CancelSortieCommand { get; }
        //public ICommand LoadEmplacementsCommand { get; }

        public ICommand AddNoteCommand { get; }
        public ICommand CancelNoteCommand { get; }



        public PlantuleFicheViewModel()
        {
            _qrCodeService = new QRCodeService();
            _pdfService = new PdfService();
            _alertService = AlertService.Instance;

            SanteOptions = new ObservableCollection<string> { "Bon", "Moyen", "Mauvais", "En danger" };
            StadeOptions = new ObservableCollection<string> { "Initiation", "Micro dissection", "Magenta", "Double magenta", "Hydroponie" };
            Emplacements = new ObservableCollection<Emplacement>();
            SortieTypeOptions = new ObservableCollection<string> { "Destruction par autoclave", "Transfert client", "Transfert autre centre", "Transfert pour analyse", "Analyse", "Contamination", "Limitation de la licence", "Perte de l'échantillon", "Plantules n'ont pas survécu à l'expérience", "Autre (indiquer la raison dans note)" };
            NoteRappelDate = DateTime.Now.AddDays(7);
            NoteText = string.Empty;

            

            SaveCommand = new Command(async () => await SaveChangesAsync());
            CancelCommand = new Command(CancelChanges);

            LoadEventsCommand = new Command(async () => await LoadEventsAsync());
            LoadNotesCommand = new Command(async () => await LoadNotesAsync());
            NavigateBackCommand = new Command(async () => await NavigateBackAsync());
            GeneratePdfCommand = new Command(async () => await GeneratePdfAsync());
            SortirPlantuleCommand = new Command(async () => await SortirPlantuleAsync());
            CancelSortieCommand = new Command(CancelSortie);
            AddNoteCommand = new Command(async () => await AddNoteAsync());
            CancelNoteCommand = new Command(CancelNote);

            LoadEmplacementsCommand = new Command(async () => await LoadEmplacementsAsync());
        }

        public PlantuleFicheViewModel(PlantuleDetail plantule) : this()
        {
            Plantule = plantule;
            SelectedSante = plantule.Sante;
            SelectedStade = plantule.Stade;
            SortieDate = DateTime.Now;
            SortieType = SortieTypeOptions.First();
            SortieObservation = string.Empty;

            SelectedEmplacement = Emplacements.FirstOrDefault(e => e.EmplacementId == plantule.EmplacementId);
        }

        public ICommand LoadEmplacementsCommand { get; }

        private async Task LoadEmplacementsAsync()
        {
            try
            {
                var emplacements = await ApiService.GetEmplacementsAsync();
                Emplacements.Clear();
                foreach (var emplacement in emplacements)
                {
                    Emplacements.Add(emplacement);
                }
                if (Plantule != null)
                {
                    SelectedEmplacement = Emplacements.FirstOrDefault(e => e.EmplacementId == Plantule.EmplacementId);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Erreur lors du chargement des emplacements: {ex.Message}";
            }
        }

        private void GenerateQrImage()
        {
            if (Plantule != null && !string.IsNullOrEmpty(Plantule.Slug))
            {
                var qrCodeBytes = _qrCodeService.GenerateQRCode(Plantule.Slug);
                QrImage = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
            }
        }

        private async Task LoadEventsAsync()
        {
            if (Plantule != null)
            {
                try
                {
                    var events = await ApiService.GetEventsByPlantuleIdAsync(Plantule.PlantuleId);
                    Events.Clear();
                    foreach (var eventItem in events)
                    {
                        Events.Add(eventItem);
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Erreur lors du chargement des événements: {ex.Message}";
                }
            }
        }

        private async Task LoadNotesAsync()
        {
            if (Plantule != null)
            {
                try
                {
                    var notes = await ApiService.GetNotesByPlantuleIdAsync(Plantule.PlantuleId);
                    Notes.Clear();
                    foreach (var noteItem in notes)
                    {
                        Notes.Add(noteItem);
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Erreur lors du chargement des notes: {ex.Message}";
                }
            }
        }

        private async Task SaveChangesAsync()
        {
            if (Plantule == null)
            {
                ErrorMessage = "La plantule est null.";
                return;
            }

            bool santeChanged = Plantule.Sante != SelectedSante;
            bool stadeChanged = Plantule.Stade != SelectedStade;
            bool emplacementChanged = Plantule.EmplacementId != SelectedEmplacement?.EmplacementId;

            if (!santeChanged && !stadeChanged && !emplacementChanged)
            {
                await _alertService.ShowAlert("Aucune modification", "Les nouvelles valeurs sont identiques aux anciennes.", "OK");
                return;
            }

            bool isModified = false;

            if (santeChanged)
            {
                var response = await ApiService.UpdatePlantuleSanteAsync(Plantule.PlantuleId, new UpdateSanteRequest { Sante = SelectedSante });
                if (response == "Élément mis à jour avec succès.") isModified = true;
            }

            if (stadeChanged)
            {
                var response = await ApiService.UpdatePlantuleStadeAsync(Plantule.PlantuleId, new UpdateStadeRequest { Stade = SelectedStade });
                if (response == "Élément mis à jour avec succès.") isModified = true;
            }

            if (emplacementChanged)
            {
                var response = await ApiService.UpdatePlantuleEmplacementAsync(Plantule.PlantuleId, new UpdateEmplacementRequest { EmplacementId = SelectedEmplacement.EmplacementId });
                if (response == "Élément mis à jour avec succès.") isModified = true;
            }

            if (isModified)
            {
                await _alertService.ShowAlert("Succès", "Les modifications ont été enregistrées.", "OK");
                await LoadEventsAsync();
                await LoadNotesAsync();
            }
        }


        private void CancelChanges()
        {
            SelectedSante = Plantule.Sante;
            SelectedStade = Plantule.Stade;
            SelectedEmplacement = Emplacements.FirstOrDefault(e => e.EmplacementId == Plantule.EmplacementId);
        }

        private async Task NavigateBackAsync()
        {
            MessagingCenter.Send(this, "RefreshEmplacements");
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async Task SortirPlantuleAsync()
        {
            try
            {
                var response = await ApiService.UpdatePlantuleSortieAsync(Plantule.PlantuleId, new UpdateSortieRequest
                {
                    SortieDate = SortieDate,
                    SortieType = SortieType,
                    SortieObservation = SortieObservation
                });

                if (response == "Élément mis à jour avec succès.")
                {
                    await _alertService.ShowAlert("Succès", "La plantule a été sortie avec succès.", "OK");
                    await LoadEventsAsync();
                    await LoadNotesAsync();
                }
                else
                {
                    await _alertService.ShowAlert("Erreur", "Erreur lors de la sortie de la plantule.", "OK");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Erreur lors de la sortie de la plantule: {ex.Message}";
            }
        }

        private void CancelSortie()
        {
            SortieDate = DateTime.Now;
            SortieType = SortieTypeOptions.First();
            SortieObservation = string.Empty;
        }

        private async Task AddNoteAsync()
        {
            if (Plantule == null)
            {
                ErrorMessage = "La plantule est null.";
                return;
            }

            var newNote = new Note
            {
                NoteTexte = NoteText,
                NoteRappelDate = NoteRappelDate,
                NoteDate = DateTime.Now,
                PlantuleId = Plantule.PlantuleId
            };

            try
            {
                await ApiService.AddNoteAsync(newNote);
                await LoadNotesAsync();
                CancelNote();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Erreur lors de l'ajout de la note: {ex.Message}";
            }
        }

        private void CancelNote()
        {
            NoteText = string.Empty;
            NoteRappelDate = DateTime.Now.AddDays(7);
        }


        private async Task GeneratePdfAsync()
        {
            if (Plantule == null)
            {
                ErrorMessage = "La plantule est null.";
                return;
            }

            if (string.IsNullOrEmpty(Plantule.Slug))
            {
                ErrorMessage = "Le slug de la plantule est vide.";
                return;
            }

            try
            {
                var qrCodeBytes = _qrCodeService.GenerateQRCode(Plantule.Slug);
                if (qrCodeBytes == null)
                {
                    ErrorMessage = "Erreur lors de la génération du QR code.";
                    return;
                }

                var pdfBytes = _pdfService.GeneratePdf(Plantule.Slug, "Fiche Plantule", qrCodeBytes);
                if (pdfBytes == null)
                {
                    ErrorMessage = "Erreur lors de la génération du PDF.";
                    return;
                }

                var fileName = $"{Plantule.Slug}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                var filePath = Path.Combine(FileSystem.CacheDirectory, fileName);

                await File.WriteAllBytesAsync(filePath, pdfBytes);

                // Utiliser le service d'alerte pour afficher l'alerte
                await _alertService.ShowAlert("PDF généré", $"Le fichier PDF a été généré et sauvegardé sous le nom {fileName}.", "OK");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Erreur lors de la génération du PDF: {ex.Message}";
            }
        }
    }
}
