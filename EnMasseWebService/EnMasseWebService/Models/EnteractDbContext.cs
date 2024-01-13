using EnMasseWebService.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EnMasseWebService.Models
{
    public class EnteractDbContext: DbContext
    {
        public EnteractDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public EnteractDbContext()
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Cafe> Cafes { get; set; }
        public DbSet<CafeUser> CafeUsers { get; set; }
    }
}

