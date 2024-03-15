using EnMasseWebService.Models;
using EnMasseWebService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnMasseWebService.Services
{
    public class CafeService
    {
        private readonly EnteractDbContext _enteractDbContext;
        public CafeService(EnteractDbContext enteractDbContext)
        {
            _enteractDbContext = enteractDbContext;
        }

        public async Task<List<Cafe>> GetAllCafesAsync(){
            return await _enteractDbContext.Cafes.ToListAsync();
        }

        public async Task<bool> AddUserToCafeAsync(Guid cafeId, Guid userId)
        {
            try
            {
                var cafeUser = new CafeUser
                {
                    CafeId = cafeId,
                    UserId = userId
                };

                await _enteractDbContext.CafeUsers.AddAsync(cafeUser);
                await _enteractDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                // Handle exceptions or log errors as necessary
                return false;
            }
        }
    }
}
