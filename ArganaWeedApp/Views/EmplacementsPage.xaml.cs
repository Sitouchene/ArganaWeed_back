namespace ArganaWeedApp.Views;

public partial class EmplacementsPage : ContentPage
{
	public EmplacementsPage()
	{
		InitializeComponent();
	}

    private async void OnEmplacementsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//EmplacementsPage");
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//EmplacementNewPage");
    }
}