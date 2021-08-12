using Microsoft.EntityFrameworkCore;

namespace UseSCIM.Host.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Email> Emails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=SCIM1;User=sa;Password=!MySecretTemplafyPassword1");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Email>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Emails);
        }
    }
}
