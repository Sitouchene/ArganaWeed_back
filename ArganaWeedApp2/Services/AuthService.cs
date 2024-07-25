using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ArganaWeedApp2.Models;

namespace ArganaWeedApp2.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _client;

        public AuthService(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("ArganaWeedApi");
        }

        public async Task<AuthResponse> LoginAsync(string email, string password)
        {
            var loginRequest = new AuthRequest { Email = email, Password = password };
            var response = await _client.PostAsJsonAsync("auth/login", loginRequest);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AuthResponse>();
        }

        public async Task LogoutAsync()
        {
            // La déconnexion côté client pourrait simplement consister à supprimer le token stocké localement.
            // Si le serveur requiert une logique de déconnexion, vous devrez implémenter ici un appel API.
            // Exemple : await _client.PostAsync("auth/logout", null);
            throw new NotImplementedException("La déconnexion doit être gérée selon les besoins de votre application.");
        }

        public async Task<string> RefreshTokenAsync(string token)
        {
            var refreshRequest = new { Token = token };
            var response = await _client.PostAsJsonAsync("auth/refresh", refreshRequest);
            response.EnsureSuccessStatusCode();
            var newToken = await response.Content.ReadFromJsonAsync<string>(); // Assurez-vous que l'API renvoie le nouveau token correctement.
            return newToken;
        }
    }
}
