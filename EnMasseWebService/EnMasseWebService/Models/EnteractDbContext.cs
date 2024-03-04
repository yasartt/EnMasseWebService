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
                .HasOne(x => x.User1)
                .WithMany(y => y.UserContacts)
                .HasForeignKey(z=> z.User1Id)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<UserContacts>()
                .HasOne(x => x.User2)
                .WithMany(y => y.ContactUsers)
                .HasForeignKey(z => z.User2Id)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<ContactRequest>()
                .HasOne(x => x.Sender)
                .WithMany(y => y.SentRequests)
                .HasForeignKey(z => z.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<ContactRequest>()
                .HasOne(x => x.Receiver)
                .WithMany(y => y.ReceivedRequests)
                .HasForeignKey(z => z.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Cafe>()
                .Property(p => p.CafeId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .Property(p => p.UserId)
                .ValueGeneratedOnAdd();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Cafe> Cafes { get; set; }
        public DbSet<CafeUser> CafeUsers { get; set; }
        public DbSet<DailyImage> DailyImages { get; set; }
        public DbSet<Daily> Dailies { get; set; }
        //public DbSet<DailyType> DailyTypes { get; set; }
        public DbSet<UserContacts> UserContacts { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }

    }
}

