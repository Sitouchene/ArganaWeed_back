using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ArganaWeedApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ArganaWeedApi.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _secret;
        private readonly string _expDate;

        public JwtService(IConfiguration config)
        {
            _secret = config["Jwt:Key"];
            _expDate = config["Jwt:ExpireMinutes"];
        }

        public string GenerateSecurityToken(string email, List<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email)
            };

            // Ajouter les rôles aux revendications
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public JwtTokenInfo DecodeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(token))
            {
                throw new ArgumentException("Le token fourni n'est pas un JWT valide.");
            }

            try
            {
                var jwtToken = handler.ReadJwtToken(token);

                // Modifier ici pour utiliser "unique_name" au lieu de ClaimTypes.Name
                var emailClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name");
                if (emailClaim == null)
                {
                    throw new SecurityTokenValidationException("Email non trouvé dans le token.");
                }

                var roles = jwtToken.Claims
                                     .Where(claim => claim.Type == ClaimTypes.Role)
                                     .Select(claim => claim.Value)
                                     .ToList();

                return new JwtTokenInfo
                {
                    Email = emailClaim.Value,
                    Roles = roles
                };
            }
            catch (Exception ex)
            {
                throw new SecurityTokenValidationException("Erreur lors de la décodification du token JWT: " + ex.Message);
            }
        }

    }
}
