using ArganaWeedAppDevEx.ViewModels;
using DevExpress.Maui.CollectionView;

namespace ArganaWeedAppDevEx.Views
{
    public partial class MainPage : Shell
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Current.GoToAsync("//LoginPage");
        }

        void OnCloseClicked(object sender, EventArgs e)
        {
            Current.FlyoutIsPresented = false;
        }
    }
}