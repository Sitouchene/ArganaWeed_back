using ArganaWeedAppDevEx.Models;
using System.Net.Http;

namespace ArganaWeedAppDevEx.Services
{
    public class UserApiService : ApiService<User>
    {
        public UserApiService(HttpClient httpClient, ApiConfiguration apiConfiguration)
            : base(httpClient, $"{apiConfiguration.BaseUrl}/api/users")
        {
        }
    }
}
