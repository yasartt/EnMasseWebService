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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserContacts>()
                .HasOne(x => x.User)
                .WithMany(y => y.UserContacts)
                .HasForeignKey(z=> z.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<UserContacts>()
                .HasOne(x => x.Contact)
                .WithMany(y => y.ContactUsers)
                .HasForeignKey(z => z.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Cafe> Cafes { get; set; }
        public DbSet<CafeUser> CafeUsers { get; set; }
        public DbSet<CafeMessage> CafeMessages { get; set; }

        public DbSet<Daily> Dailies { get; set; }
        public DbSet<DailyType> DailyTypes { get; set; }
        public DbSet<UserContacts> UserContacts { get; set; }

    }
}

