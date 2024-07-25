using ArganaWeedRest.ViewModels;
namespace ArganaWeedRest.Views;

public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashboardViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}