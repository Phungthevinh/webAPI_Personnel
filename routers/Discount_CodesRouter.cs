using Npgsql;
using WebAPI.Controllers;
using WebAPI.models;

namespace WebAPI.routers
{
    public class Discount_CodesRouter
    {
        public Discount_CodesRouter(WebApplication app, NpgsqlDataSource db)
        {
            app.MapPost("/them-moi-ma-giam-gia", (Discount_Codes discount_codes) =>
            {
                Discount_CodesController discount_CodesController = new Discount_CodesController();
                return discount_CodesController.themmoimagiamgia(discount_codes,db);
            });

            //kích hoạt mã giảm giá
            app.MapPatch("/kich-hoat-ma-giam-gia", (Discount_Codes discount_codes) =>
            {
                Discount_CodesController discount_CodesController = new Discount_CodesController();
                return discount_CodesController.kichhoatmagiamgia(discount_codes, db);
            });

            //khóa mã giảm giá
            app.MapPatch("/khoa-ma-giam-gia", (Discount_Codes discount_codes) =>
            {
                Discount_CodesController discount_CodesController = new Discount_CodesController();
                return discount_CodesController.khoamagiamgia(discount_codes, db);
            });
        }
    }
}
