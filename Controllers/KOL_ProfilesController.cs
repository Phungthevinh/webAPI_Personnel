//                         
//
//       69696969                         69696969
//    6969    696969                   696969    6969
//  969    69  6969696               6969  6969     696
// 969        696969696             696969696969     696
//969        69696969696           6969696969696      696
//696      9696969696969           969696969696       969
// 696     696969696969             969696969        969
//  696     696  96969      _=_      9696969  69    696
//    9696    969696      q(-_-)p      696969    6969
//       96969696         '_) (_`         69696969
//          96            /__/  \            69
//          69          _(<_   / )_          96
//         6969        (__\_\_|_/__)        9696
//
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Security.Claims;
using WebAPI.DTOs.used_discount_code;
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
        //thêm mới profile cho KOL
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

        //xem tất cả các mã và số lượng sử dụng mã  của từng KOC
        public async Task<IResult> CouponReportsController(ClaimsPrincipal claims)
        {
            try
            {
                KocPerformanceService kocPerformanceService = new KocPerformanceService(_dbContext);
                var koc = await kocPerformanceService.getCouponUsageSummary(claims);
                return Results.Ok(koc);
            }catch(Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
        //xem doanh thu mang về của từng KOC
        //xem số tiền chiếc khấu, và % chiếc khấu cho từng KOC


    }
}
