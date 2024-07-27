namespace ArganaWeedApp.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;


        bool isAuthenticated = await AuthenticateUserAsync(username, password);

        if (isAuthenticated)
        {
            // Rediriger vers la page MenuPage après une authentification réussie
            await Shell.Current.GoToAsync("//MenuPage");
        }
        else
        {
            MessageLabel.Text = "Nom d'utilisateur ou mot de passe incorrect.";
        }


        /*
        // Ajouter la logique d'authentification ici
        bool isAuthenticated = await AuthenticateUserAsync(username, password);

        if (isAuthenticated)
        {
            // Rediriger vers la page d'accueil après une authentification réussie
            await Navigation.PushAsync(new AcceuilPage());
        }
        else
        {
            MessageLabel.Text = "Nom d'utilisateur ou mot de passe incorrect.";
        }
        */
    }

    private Task<bool> AuthenticateUserAsync(string username, string password)
    {
        // Ajouter votre logique d'authentification ici
        // Retourner true si l'authentification est réussie, sinon false
        //
        return Task.FromResult(username == "salim" && password == "123Soleil");
    }

    /*private async void OnPlantulesButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//PlantulesPage"); 
    }
    private async void OnEmplacementsButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//EmplacementsPage");
    }*/
    
}