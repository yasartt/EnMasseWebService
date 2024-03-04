using EnMasseWebService.Models.Entities;
using EnMasseWebService.Models.DTOs;
using EnMasseWebService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EnMasseWebService.Services
{
    public class UserService
    {
        private readonly EnteractDbContext _enteractDbContext;

        public UserService(EnteractDbContext enteractDbContext)
        {
            _enteractDbContext = enteractDbContext;
        }

        /**public async Task<bool> SendContactRequestAsync(ContactRequestDTO contactRequestDTO)
        {

        } */
    }
}
