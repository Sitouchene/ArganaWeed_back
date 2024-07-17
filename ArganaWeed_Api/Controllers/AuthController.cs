using ArganaWeed_Api.Models;
using ArganaWeed_Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ArganaWeed_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly JwtService _jwtService;

        public AuthController(IConfiguration config, JwtService jwtService)
        {
            _config = config;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthRequest request)
        {
            // Remplacez par votre logique d'authentification (ex. vérification des utilisateurs en DB)
            if (request.Email == "test@example.com" && request.Password == "password")
            {
                var token = _jwtService.GenerateSecurityToken(request.Email);
                return Ok(new AuthResponse { Token = token });
            }

            return Unauthorized();
        }
    }
}
