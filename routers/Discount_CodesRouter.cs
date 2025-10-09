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
            //KOC sẽ thêm mới mã giảm giá của mình và chờ được duyệt
            app.MapPost("/them-moi-ma-giam-gia", [Authorize] (discount_codes discount_codes, dbContext dbContext) =>
            {
                Discount_CodesController discount_CodesController = new Discount_CodesController(dbContext);
                return discount_CodesController.themmoimagiamgia(discount_codes);
            });

            //nhân viên hoặc admin kích hoặt mã giảm giá cho người dùng
            app.MapPatch("/kich-hoat-ma-giam-gia", [Authorize] (discount_codes discount_codes, dbContext dbContext) =>
            {
                Discount_CodesController discount_CodesController = new Discount_CodesController(dbContext);
                return discount_CodesController.kichhoatmagiamgia(discount_codes);
            });

            //khóa mã giảm giá
            app.MapPatch("/khoa-ma-giam-gia", [Authorize] (discount_codes discount_codes, dbContext db) =>
            {
                Discount_CodesController discount_CodesController = new Discount_CodesController(db);
                return discount_CodesController.khoamagiamgia(discount_codes);
            });
        }
    }
}
