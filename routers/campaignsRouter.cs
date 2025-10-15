using Microsoft.AspNetCore.Authorization;
using WebAPI.Controllers;
using WebAPI.models;
using WebAPI.Services;

namespace WebAPI.routers
{
    public class campaignsRouter
    {
        public campaignsRouter(WebApplication app)
        {
            //tạo sự kiện giảm giá mới
            app.MapPost("/tao-su-kien-moi", [Authorize] (campaigns campaigns, dbContext dbContext) =>
            {
                campaignsController taoSuKienMoi = new campaignsController(dbContext);
                return taoSuKienMoi.taoSuKienDeCTVacpMa(campaigns);
            });
            //xem tất cả các sự kiện giảm giá
            app.MapGet("/su-kien-giam-gia", [Authorize] (dbContext dbContext) =>
            {
                campaignsController xemsukien = new campaignsController(dbContext);
                return xemsukien.xemtatcasukien();
            });
        }
    }
}
