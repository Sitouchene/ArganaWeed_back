namespace ArganaWeedApp.Views;

public partial class MenuPage : ContentPage
{
	public MenuPage()
	{
		InitializeComponent();
	}

    private async void OnPlantulesClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//PlantulesPage");
    }

    private async void OnEmplacementsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//EmplacementsPage");
    }
}