using System.Threading.Tasks;
using System.Windows.Input;
using ArganaWeedApp.Models;
using ArganaWeedApp.Services;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.ViewModels
{
    public class VarieteNewViewModel : BindableObject
    {
        private string _varieteCode;
        public string VarieteCode
        {
            get => _varieteCode;
            set
            {
                _varieteCode = value;
                OnPropertyChanged();
            }
        }


        private string _varieteNom;
        public string VarieteNom
        {
            get => _varieteNom;
            set
            {
                _varieteNom = value;
                OnPropertyChanged();
            }
        }

        private string _varieteCategorie;
        public string VarieteCategorie
        {
            get => _varieteCategorie;
            set
            {
                _varieteCategorie = value;
                OnPropertyChanged();
            }
        }


        private string _varieteDescription;
        public string VarieteDescription
        {
            get => _varieteDescription;
            set
            {
                _varieteDescription = value;
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

        private string _someParameter;
        public string SomeParameter
        {
            get => _someParameter;
            set
            {
                _someParameter = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public VarieteNewViewModel()
        {
            SaveCommand = new Command(async (param) => await SaveVarieteAsync(param));
            CancelCommand = new Command(async (param) => await CancelAsync(param));
        }


        private async Task SaveVarieteAsync(object param)
        {
            SomeParameter = param as string; // Capture the command parameter

            if (string.IsNullOrWhiteSpace(VarieteCode) || string.IsNullOrWhiteSpace(VarieteNom) || string.IsNullOrWhiteSpace(VarieteCategorie) || string.IsNullOrWhiteSpace(VarieteDescription))
            {
                ErrorMessage = "Tous les champs doivent être remplis.";
                return;
            }

            var variete = new Variete
            {
                VarieteCode = VarieteCode,
                VarieteDescription = VarieteDescription
            };

            await ApiService.AddVarieteAsync(variete);
            await Application.Current.MainPage.Navigation.PopAsync();
        }


        private async Task CancelAsync(object param)
        {
            SomeParameter = param as string; // Capture the command parameter
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
