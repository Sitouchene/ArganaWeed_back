using System.Collections.Generic;
using ArganaWeedApi.Models;
namespace ArganaWeedApi.Services
{
    public interface IJwtService
    {
        string GenerateSecurityToken(string email, List<string> roles);
        JwtTokenInfo DecodeToken(string token);

    }
}
