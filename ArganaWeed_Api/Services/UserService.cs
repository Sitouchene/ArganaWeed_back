using ArganaWeedApp.Data;
using ArganaWeedApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArganaWeedApp.Services
{
    public class UserService : IUserService
    {
        private readonly ArganaWeedDbContext _context;

        public UserService(ArganaWeedDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Authenticated, string Message, string CurrentUser, int? UserId, List<string> Roles)> AuthenticateUserAsync(string login, string password)
        {
            var parameters = new[]
            {
                new SqlParameter("@Login", login),
                new SqlParameter("@UserPassword", password)
            };

            var results = await _context.AuthResults
                .FromSqlRaw("EXEC [dbo].[authenticateUser] @Login, @UserPassword", parameters)
                .ToListAsync();

            var result = results.FirstOrDefault();

            if (result == null || !result.Authenticated)
            {
                return (false, result?.Message ?? "Authentication failed", null, null, null);
            }

            var roles = new List<string>();
            if (result.IsAdministrator) roles.Add("Administrator");
            if (result.IsOwner) roles.Add("Owner");
            if (result.IsAgent) roles.Add("Agent");
            if (result.IsViewer) roles.Add("Viewer");

            return (true, result.Message, result.CurrentUser, result.UserId, roles);
        }
    }

    
}
