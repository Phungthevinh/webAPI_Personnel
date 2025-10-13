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
            app.MapPost("/tao-su-kien-moi", (campaigns campaigns, dbContext dbContext) =>
            {
                campaignsController taoSuKienMoi = new campaignsController(dbContext);
                return taoSuKienMoi.taoSuKienDeCTVacpMa(campaigns);
            });
            //xem tất cả các sự kiện giảm giá
            app.MapGet("/su-kien-giam-gia", (dbContext dbContext) =>
            {

            });
        }
    }
}
