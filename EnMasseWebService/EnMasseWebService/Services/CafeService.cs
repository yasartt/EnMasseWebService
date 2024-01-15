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
    }
}
