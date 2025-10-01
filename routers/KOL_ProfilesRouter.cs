//using Microsoft.AspNetCore.Authorization;
//using Npgsql;
//using WebAPI.Controllers;
//using WebAPI.models;

//namespace WebAPI.routers
//{
//    public class KOL_ProfilesRouter
//    {
//        public KOL_ProfilesRouter(WebApplication app, NpgsqlDataSource db)
//        {
//            app.MapPatch("/them-thong-tin-KOL", [Authorize] (KOL_Profiles KOL) => 
//            {
//                KOL_ProfilesController kOL_ProfilesController = new KOL_ProfilesController();
//                return kOL_ProfilesController.ThemmoiprofileKOL(db, KOL);
//            });
//        }
//    }
//}
