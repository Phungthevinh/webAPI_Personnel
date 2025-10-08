using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.models;

namespace WebAPI.Services
{
    public class dbContext(DbContextOptions<dbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Users> users { get; set; }
        public DbSet<ai_prompts> ai_prompts { get; set; }
        public DbSet<KOL_Profiles> kol_profiles { get; set; }
        public DbSet<Roles> roles { get; set; }
        public DbSet<Permissions> permissions { get; set; }
        public DbSet<user_roles> user_roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<user_roles>()
                .HasKey(ur => new { ur.user_id, ur.role_id });
        }

    }
}
