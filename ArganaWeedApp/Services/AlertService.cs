using System.Threading.Tasks;
using Microsoft.Maui.Controls;

public class AlertService
{
    private static readonly Lazy<AlertService> lazy = new Lazy<AlertService>(() => new AlertService());

    public static AlertService Instance { get { return lazy.Value; } }

    private AlertService() { }

    private Page CurrentPage => Application.Current.MainPage;

    public Task ShowAlert(string title, string message, string cancel)
    {
        return CurrentPage.DisplayAlert(title, message, cancel);
    }
}
