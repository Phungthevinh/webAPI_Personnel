using Microsoft.AspNetCore.Authorization;
using Npgsql;
using WebAPI.Controllers;
using WebAPI.models;
using WebAPI.Services;

namespace WebAPI.routers
{
    public class KOL_ProfilesRouter
    {
        public KOL_ProfilesRouter(WebApplication app)
        {
            //dùng để thêm thông tin và chỉnh sửa thông tin
            app.MapPatch("/them-thong-tin-KOL", [Authorize] (KOL_Profiles KOL, dbContext db) =>
            {
                KOL_ProfilesController kOL_ProfilesController = new KOL_ProfilesController(db);
                return kOL_ProfilesController.ThemmoiprofileKOL( KOL);
            });
        }
    }
}
