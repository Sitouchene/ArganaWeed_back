namespace ArganaWeedApp.Views;

public partial class EmplacementNewPage : ContentPage
{
	public EmplacementNewPage()
	{
		InitializeComponent();
	}
    private async void OnQuitClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//EmplacementsPage");
    }
   


}