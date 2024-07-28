namespace ArganaWeedApp.Views;

public partial class PlantulesPage : ContentPage
{
	public PlantulesPage()
	{
		InitializeComponent();
        
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior
        {
            Command = new Command(async () => await Shell.Current.GoToAsync("..")) // Retour à MenuPage
        });
    }
}