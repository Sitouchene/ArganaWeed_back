using ArganaWeedApp.ViewModels;
namespace ArganaWeedApp.Views;

public partial class EmplacementNewPage : ContentPage
{
    public EmplacementNewPage()
    {
        InitializeComponent();
        //BindingContext = new EmplacementNewViewModel();
    }


    /*private async void OnQuitClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EmplacementsPage());
    }*/

    private void OnSaveButton_Clicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as EmplacementNewViewModel;
        viewModel?.SaveCommand.Execute(null);
    }
    private void OnCancelButton_CLicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as EmplacementNewViewModel;
        viewModel?.CancelCommand.Execute(null);
    }


}