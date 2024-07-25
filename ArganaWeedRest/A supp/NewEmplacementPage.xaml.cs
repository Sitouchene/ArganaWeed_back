using ArganaWeedAppDevEx.ViewModels;

namespace ArganaWeedAppDevEx.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewEmplacementPage : ContentPage
    {
        public NewEmplacementPage()
        {
            InitializeComponent();
            BindingContext = new NewEmplacementViewModel();
        }
    }
}
