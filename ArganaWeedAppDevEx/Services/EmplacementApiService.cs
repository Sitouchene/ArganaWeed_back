using ArganaWeedAppDevEx.Models;
using System.Net.Http;

namespace ArganaWeedAppDevEx.Services
{
    public class EmplacementApiService : ApiService<Emplacement>
    {
        public EmplacementApiService(HttpClient httpClient, ApiConfiguration apiConfiguration)
            : base(httpClient, $"{apiConfiguration.BaseUrl}/api/emplacements")
        {
        }
    }
}
