using WebAPI.Controllers;
using WebAPI.models;
using WebAPI.Services;

namespace WebAPI.routers
{
    public class campaignsRouter
    {
        public campaignsRouter(WebApplication app)
        {
            app.MapPost("/tao-su-kien-moi", (campaigns campaigns, dbContext dbContext) =>
            {
                campaignsController taoSuKienMoi = new campaignsController(dbContext);
                return taoSuKienMoi.taoSuKienDeCTVacpMa(campaigns);
            });
        }
    }
}
