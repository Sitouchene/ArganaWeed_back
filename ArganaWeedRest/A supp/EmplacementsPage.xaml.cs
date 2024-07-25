using ArganaWeedAppDevEx.ViewModels;

namespace ArganaWeedAppDevEx.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmplacementsPage : ContentPage
    {
        public EmplacementsPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new EmplacementsViewModel();
        }

        EmplacementsViewModel ViewModel { get; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
    }
}
