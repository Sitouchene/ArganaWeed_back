using System.Collections.Generic;
using ArganaWeedApp.Models;
namespace ArganaWeedApp.Services
{
    public interface IJwtService
    {
        string GenerateSecurityToken(string email, List<string> roles);
        JwtTokenInfo DecodeToken(string token);

    }
}
