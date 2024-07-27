
//using ArganaWeedModels;
using ArganaWeedApp.Models;
using ArganaWeedApp.DTOs;
using ArganaWeedApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArganaWeedApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

         [HttpPost("login")]
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            LoginResponse loginResponse = new LoginResponse();
          
            var (authenticated, message, currentUser, userId, roles) = await _userService.AuthenticateUserAsync(request.Email, request.Password);

            if (authenticated)
            {
                var token = _jwtService.GenerateSecurityToken(request.Email, roles);
                loginResponse.Token = token;
                loginResponse.Success = true;
                loginResponse.Message = message;
                loginResponse.UserId = userId ?? 0;
                loginResponse.Roles = roles;
            }
            else{
                loginResponse.Success = false;
                loginResponse.Message = message;
            }
            return loginResponse;
        }

        

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Renvoyer simplement une confirmation de la déconnexion.
            return Ok(new { Message = "Déconnexion réussie" });
        }

        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            // Valider le token existant et extraire les informations nécessaires (ici simplifié)
            var userInfo = _jwtService.DecodeToken(tokenRequest.Token); // Supposons que cette méthode décode le token
            if (userInfo == null)
            {
                return Unauthorized(new { Message = "Invalid token" });
            }

            var newToken = _jwtService.GenerateSecurityToken(userInfo.Email, userInfo.Roles);
            return Ok(new { Token = newToken });
        }

    }
}
