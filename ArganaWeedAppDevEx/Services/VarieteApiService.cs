using ArganaWeedAppDevEx.Models;
using System.Net.Http;

namespace ArganaWeedAppDevEx.Services
{
    public class VarieteApiService : ApiService<Variete>
    {
        public VarieteApiService(HttpClient httpClient, ApiConfiguration apiConfiguration)
            : base(httpClient, $"{apiConfiguration.BaseUrl}/api/varietes")
        {
        }
    }
}
