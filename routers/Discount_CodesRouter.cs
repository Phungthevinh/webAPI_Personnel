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
            app.MapPost("/them-moi-ma-giam-gia-theo-su-kien", [Authorize] (discount_codes discount_codes, dbContext dbContext) =>
            {
                Discount_CodesController discount_CodesController = new Discount_CodesController(dbContext);
                return discount_CodesController.themmoimagiamgiatheoSukien(discount_codes);
            });
            //xem mã giảm giá cho từng koc và sự kiện tương ứng
            app.MapGet("/ma-giam-gia-va-su-kien-tuong-ung",[Authorize] (dbContext dbContext, int kol_id) =>
            {
                Discount_CodesController xemmagiamgiavasukien = new Discount_CodesController(dbContext);
                return xemmagiamgiavasukien.maGiamGiaTuongUngSuKien(kol_id);
            });
            //trả về giá trị giảm giá khi khách hàng acp mã
            app.MapPost("/gia-tri-ma-giam", (dbContext dbContext, discount_codes discount_Codes) =>
            {
                Discount_CodesController giaTriMaGiam = new Discount_CodesController(dbContext);
                return giaTriMaGiam.giaTriMaGiam(discount_Codes);
            });
        }
    }
}
