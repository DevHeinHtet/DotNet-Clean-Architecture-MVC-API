using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public virtual DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AppUserConfiguration).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);

            // Data Seeding
            DataSeeder.SeedRoles(builder);
            DataSeeder.SeedAppUsers(builder);
        }
    }
}
