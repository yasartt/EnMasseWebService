using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using EnMasseWebService.Models;
using EnMasseWebService.Models.DTOs;

namespace EnMasseWebService.Services
{
    public class AuthService
    {
        private readonly EnteractDbContext _context;
        public AuthService(EnteractDbContext enteractDbContext) {
            _context = enteractDbContext;
        }

        public async Task<int> LoginAsync(LoginDTO loginDTO)
        {
            try
            {
                if (loginDTO == null)
                {
                    return -1;
                }
                var userName = loginDTO.userName;
                var password = loginDTO.password;

                var user = await _context.Users.SingleOrDefaultAsync(q => q.UserName == userName);

                if (user == null) {
                    return -2;
                }
                
                if(password != user.Password)
                {
                    return -3;
                }

                return 0;
            }
            catch {
                return -5;
            }
        }
    }
}
