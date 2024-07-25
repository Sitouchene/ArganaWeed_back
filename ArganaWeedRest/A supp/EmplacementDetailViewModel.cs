using ArganaWeedAppDevEx.Models;
using System.Web;

namespace ArganaWeedAppDevEx.ViewModels
{
    public class EmplacementDetailViewModel : BaseViewModel, IQueryAttributable
    {
        public const string ViewName = "EmplacementDetailPage";

        string emplacementCode;
        string emplacementDescription;

        public string EmplacementId { get; set; }

        public string EmplacementCode
        {
            get => this.emplacementCode;
            set => SetProperty(ref this.emplacementCode, value);
        }

        public string EmplacementDescription
        {
            get => this.emplacementDescription;
            set => SetProperty(ref this.emplacementDescription, value);
        }

        public async Task LoadEmplacementId(string emplacementId)
        {
            try
            {
                var emplacement = await DataStore.GetItemAsync(emplacementId);
                EmplacementId = emplacement.EmplacementId;
                EmplacementCode = emplacement.EmplacementCode;
                EmplacementDescription = emplacement.EmplacementDescription;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Failed to Load Emplacement");
            }
        }

        public override async Task InitializeAsync(object parameter)
        {
            await LoadEmplacementId(parameter as string);
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            string id = HttpUtility.UrlDecode(query["id"] as string);
            await LoadEmplacementId(id);
        }
    }
}
