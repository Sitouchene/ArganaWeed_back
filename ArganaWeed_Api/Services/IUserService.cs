using ArganaWeedApp.Models;

namespace ArganaWeedApp.Services
{
    public interface IUserService
    {
        Task<(bool Authenticated, string Message, string CurrentUser, int? UserId, List<string> Roles)> AuthenticateUserAsync(string login, string password);
    }
}
