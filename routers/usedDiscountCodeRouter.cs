using WebAPI.Controllers;
using WebAPI.models;
using WebAPI.Services;

namespace WebAPI.routers
{
    public class usedDiscountCodeRouter
    {
        public usedDiscountCodeRouter(WebApplication app)
        {
            app.MapPost("/them-ma-giam-gia-va-nguoi-su-dung", (Used_Discount_Codes used_Discount_code, dbContext dbContext) =>
            {
                UsedDiscountCodesController u = new UsedDiscountCodesController(dbContext);
                return u.nguoiDungSuDungMaGiamGia(used_Discount_code);
            });
        }
    }
}
