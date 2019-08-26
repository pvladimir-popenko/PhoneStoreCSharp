using Microsoft.EntityFrameworkCore;
using MvcApplicationCSharp5.Models;

namespace MvcApplicationCSharp5
{
    public class ApplicationDbContext : DbContext
    {
        // code first
        public DbSet<Phone> Phones { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Phone>()
                .HasOne(p => p.Company)
                .WithMany(c => c.Phones)
                .HasForeignKey(p => p.CompanyId);

            builder.Entity<Company>()
                .HasMany(c => c.Phones)
                .WithOne(p => p.Company)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
