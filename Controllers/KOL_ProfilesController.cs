using Microsoft.EntityFrameworkCore;
using Npgsql;
using WebAPI.models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class KOL_ProfilesController
    {
        private readonly dbContext _dbContext;
        public KOL_ProfilesController(dbContext db)
        {
            _dbContext = db;
        }
        public async Task<IResult> ThemmoiprofileKOL( KOL_Profiles KOL)
        {
            try
            {
                var kOL_Profiles = await _dbContext.kol_profiles
                    .Where(k => k.user_id == KOL.user_id)
                    .FirstOrDefaultAsync();
                kOL_Profiles.phone = KOL.phone;
                kOL_Profiles.niche = KOL.niche;
                kOL_Profiles.bio = KOL.bio;
                kOL_Profiles.social_media_links = KOL.social_media_links;
                await _dbContext.SaveChangesAsync();
                return Results.Ok(new {KOL.user_id});
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
