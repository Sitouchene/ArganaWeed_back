using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArganaWeedApp2.Models;

namespace ArganaWeedApp2.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Asynchronously authenticate a user with email and password.
        /// </summary>
        /// <param name="email">The email of the user attempting to log in.</param>
        /// <param name="password">The password of the user attempting to log in.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the response including token and user details.</returns>
        Task<AuthResponse> LoginAsync(string email, string password);

        /// <summary>
        /// Log out the current user by invalidating their authentication token.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task LogoutAsync();

        /// <summary>
        /// Refreshes the expired token for a user.
        /// </summary>
        /// <param name="token">The old/expired token.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a new valid token.</returns>
        Task<string> RefreshTokenAsync(string token);
    }
}
