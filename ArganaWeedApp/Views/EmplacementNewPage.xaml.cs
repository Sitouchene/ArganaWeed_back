using ArganaWeedApp.ViewModels;
namespace ArganaWeedApp.Views;

public partial class EmplacementNewPage : ContentPage
{
    public EmplacementNewPage()
    {
        InitializeComponent();
        //BindingContext = new EmplacementNewViewModel();
    }


    private async void OnQuitClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EmplacementsPage());
    }

   


}