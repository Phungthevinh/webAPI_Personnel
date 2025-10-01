using Microsoft.EntityFrameworkCore;
using WebAPI.models;

namespace WebAPI.Services
{
    public class dbContext(DbContextOptions<dbContext> options) : DbContext(options)
    {
        public DbSet<Users> users { get; set; }
        public DbSet<ai_prompts> ai_prompts { get; set; }
        public DbSet<KOL_Profiles> kol_profiles { get; set; }
    }
}
