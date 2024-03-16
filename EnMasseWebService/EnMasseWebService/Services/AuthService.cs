using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using EnMasseWebService.Models;
using EnMasseWebService.Models.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace EnMasseWebService.Services
{
    public class AuthService
    {
        private readonly EnteractDbContext _context;
        private readonly string _jwtSecretKey = "7qYWyFz8F31gmmdBhT8HT6aEg4XfNXwr"; // Replace with your actual secret key

        public AuthService(EnteractDbContext enteractDbContext) {
            _context = enteractDbContext;
        }

        public async Task<(bool, string, UserDTO?)> LoginAsync(LoginDTO loginDTO)
        {
            try
            {
                if (loginDTO == null)
                {
                    return (false, "Bad Request", null);
                }
                var userName = loginDTO.userName;
                var password = loginDTO.password;

                var user = await _context.Users.SingleOrDefaultAsync(q => q.UserName == userName);

                if (user == null) {
                    return (false, "Username Not Found", null);
                }

                if (password != user.Password)
                {
                    return (false, "Incorrect Password", null);
                }

                var returningDTO = new UserDTO()
                {
                    UserId = user.UserId,
                    UserName = userName,
                    Email = user.Email,
                };

                var token = GenerateJwtToken(returningDTO);

                return (true, token, returningDTO);

            }
            catch {
                return (false, "Server Error", null);
            }
        }

        private string GenerateJwtToken(UserDTO userDTO)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userDTO.UserName),
                new Claim(ClaimTypes.NameIdentifier, userDTO.UserId.ToString()),
                new Claim(ClaimTypes.Email, userDTO.Email),
                // Add additional claims as needed
            };

            var token = new JwtSecurityToken(
                issuer: "your-issuer", // Replace with your actual issuer
                audience: "your-audience", // Replace with your actual audience
                claims: claims,
                expires: DateTime.UtcNow.AddYears(1), // Token is valid for 1 year
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
