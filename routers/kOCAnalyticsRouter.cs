using Microsoft.AspNetCore.Authorization;
using WebAPI.Controllers;
using WebAPI.Services;

namespace WebAPI.routers
{
    public class kOCAnalyticsRouter
    {
        public kOCAnalyticsRouter(WebApplication app)
        {
            app.MapGet("/phan-tich-du-lieu-koc-va-chien-dich", [Authorize](dbContext dbContext) =>
            {
                KOCAnalyticsController kOCAnalytics = new KOCAnalyticsController(dbContext);
                return kOCAnalytics.GetKOCReport();
            });

            app.MapGet("/hoa-hong-cho-koc", (dbContext dbContext) =>
            {
                commissionService commissionService = new commissionService(dbContext);
                return commissionService.commissionForKoc();
            });
        }
    }
}
