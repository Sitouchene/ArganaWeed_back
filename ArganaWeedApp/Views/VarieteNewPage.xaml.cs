using ArganaWeedApp.ViewModels;
namespace ArganaWeedApp.Views;

public partial class VarieteNewPage : ContentPage
{
    public VarieteNewPage()
    {
        InitializeComponent();
        //BindingContext = new VarieteNewViewModel();
    }


  
    private void OnSaveButton_Clicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as VarieteNewViewModel;
        viewModel?.SaveCommand.Execute(null);
    }
    private void OnCancelButton_CLicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as VarieteNewViewModel;
        viewModel?.CancelCommand.Execute(null);
    }


}