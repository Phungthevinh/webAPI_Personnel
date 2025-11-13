using Microsoft.AspNetCore.Authorization;
using WebAPI.Controllers;
using WebAPI.Services;

namespace WebAPI.routers
{
    public class RevenueRouter
    {
        public RevenueRouter(WebApplication app)
        {
            app.MapGet("doanh-so-1-thang-cua-tung-koc-va-su-kien",[Authorize] (dbContext dbContext, DateTime dateTime) =>
            {
                RevenueController revenueController = new RevenueController(dbContext);
                return revenueController.TotalRevenue(dateTime);
            });
        }
    }
}
