using EnMasseWebService.Models.Entities;
using EnMasseWebService.Models;
using Microsoft.EntityFrameworkCore;


namespace EnMasseWebService.Services
{

    public class DailyService
    {
        private readonly EnteractDbContext _enteractDbContext;
        public DailyService(EnteractDbContext enteractDbContext)
        {
            _enteractDbContext = enteractDbContext;
        }

        /**
        public async Task<Daily> AddNewDailyAsync(..c dailyDTO)
        {

        }*/

    }
}


