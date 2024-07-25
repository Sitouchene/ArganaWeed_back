using ArganaWeedAppDevEx.ViewModels;

namespace ArganaWeedAppDevEx.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmplacementDetailPage : ContentPage
    {
        public EmplacementDetailPage()
        {
            InitializeComponent();
            BindingContext = new EmplacementDetailViewModel();
        }
    }
}
