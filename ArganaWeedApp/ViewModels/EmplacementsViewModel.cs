using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ArganaWeedApp.Models;
using ArganaWeedApp.Services;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.ViewModels
{
    public class EmplacementsViewModel : BindableObject
    {
        private ObservableCollection<Emplacement> _emplacements;
        public ObservableCollection<Emplacement> Emplacements
        {
            get => _emplacements;
            set
            {
                _emplacements = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCommand { get; }

        public EmplacementsViewModel()
        {
            Emplacements = new ObservableCollection<Emplacement>();
            RefreshCommand = new Command(async () => await LoadEmplacementsAsync());
            LoadEmplacementsAsync();
        }

        private async Task LoadEmplacementsAsync()
        {
            var emplacements = await ApiService.GetEmplacementsAsync();
            if (emplacements != null)
            {
                Emplacements.Clear();
                foreach (var emplacement in emplacements)
                {
                    Emplacements.Add(emplacement);
                }
            }
        }
    }
}
