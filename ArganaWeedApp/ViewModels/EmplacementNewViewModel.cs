using System.Threading.Tasks;
using System.Windows.Input;
using ArganaWeedApp.Models;
using ArganaWeedApp.Services;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.ViewModels
{
    public class EmplacementNewViewModel : BindableObject
    {
        private string _emplacementCode;
        public string EmplacementCode
        {
            get => _emplacementCode;
            set
            {
                _emplacementCode = value;
                OnPropertyChanged();
            }
        }

        private string _emplacementDescription;
        public string EmplacementDescription
        {
            get => _emplacementDescription;
            set
            {
                _emplacementDescription = value;
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

        public EmplacementNewViewModel()
        {
            SaveCommand = new Command(async (param) => await SaveEmplacementAsync(param));
            CancelCommand = new Command(async (param) => await CancelAsync(param));
        }


        private async Task SaveEmplacementAsync(object param)
        {
            SomeParameter = param as string; // Capture the command parameter

            if (string.IsNullOrWhiteSpace(EmplacementCode) || string.IsNullOrWhiteSpace(EmplacementDescription))
            {
                ErrorMessage = "Tous les champs doivent être remplis.";
                return;
            }

            var emplacement = new Emplacement
            {
                EmplacementCode = EmplacementCode,
                EmplacementDescription = EmplacementDescription
            };

            await ApiService.AddEmplacementAsync(emplacement);
            await Application.Current.MainPage.Navigation.PopAsync();
        }


        private async Task CancelAsync(object param)
        {
            SomeParameter = param as string; // Capture the command parameter
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
