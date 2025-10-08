using Microsoft.AspNetCore.Authorization;
using Npgsql;
using WebAPI.Controllers;
using WebAPI.models;
using WebAPI.Services;

namespace WebAPI.routers
{
    public class Discount_CodesRouter
    {
        public Discount_CodesRouter(WebApplication app)
        {
            app.MapPost("/them-moi-ma-giam-gia", [Authorize] (discount_codes discount_codes, dbContext dbContext) =>
            {
                Discount_CodesController discount_CodesController = new Discount_CodesController(dbContext);
                return discount_CodesController.themmoimagiamgia(discount_codes);
            });

            //kích hoạt mã giảm giá
            //app.MapPatch("/kich-hoat-ma-giam-gia", [Authorize] (Discount_Codes discount_codes) =>
            //{
            //    Discount_CodesController discount_CodesController = new Discount_CodesController();
            //    return discount_CodesController.kichhoatmagiamgia(discount_codes, db);
            //});

            //khóa mã giảm giá
            //app.MapPatch("/khoa-ma-giam-gia", [Authorize] (Discount_Codes discount_codes) =>
            //{
            //    Discount_CodesController discount_CodesController = new Discount_CodesController();
            //    return discount_CodesController.khoamagiamgia(discount_codes, db);
            //});
        }
    }
}
