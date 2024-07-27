namespace ArganaWeedApp.Views;

public partial class EmplacementDetailPage : ContentPage
{
	public EmplacementDetailPage()
	{
		InitializeComponent();
	}

    private async void OnQuitClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//EmplacementsPage");
    }
}