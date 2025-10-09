using Microsoft.EntityFrameworkCore;
using Npgsql;
using WebAPI.models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class Discount_CodesController
    {
        private dbContext _db;
        public Discount_CodesController(dbContext db)
        {
            _db = db;
        }

        //thêm mới mã giảm giá
        public async Task<IResult> themmoimagiamgia(discount_codes discount_Codes)
        {
            try
            {
                discount_Codes.Deactivate();
                _db.Add(new discount_codes
                {
                    code = discount_Codes.code,
                    kol_id = discount_Codes.kol_id,
                    discount_value = discount_Codes.discount_value,
                    valid_from = discount_Codes.valid_from,
                    valid_until = discount_Codes.valid_until,
                    is_active = discount_Codes.is_active
                });
                await _db.SaveChangesAsync();
                return Results.Ok();
            }catch(Exception ex)
            {
                return Results.BadRequest(ex.InnerException.Message);
            }


        }

        //kích hoạt mã giảm giá
        public async Task<IResult> kichhoatmagiamgia(discount_codes discount_Codes)
        {
            try
            {
                discount_Codes.Activate();
                var activeCode = await _db.discount_codes
                    .Where(d => d.id == discount_Codes.id)
                    .FirstOrDefaultAsync();

                activeCode.is_active = discount_Codes.is_active;

                await _db.SaveChangesAsync();
                return Results.Ok(200);
            }catch(Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        //khóa mã giảm giá
        public async Task<IResult> khoamagiamgia(discount_codes discount_Codes)
        {
            try
            {

                discount_Codes.Deactivate();
                var lockDiscount = await _db.discount_codes
                    .Where(d => d.id == discount_Codes.id)
                    .FirstOrDefaultAsync();
                lockDiscount.is_active = discount_Codes.is_active;
                await _db.SaveChangesAsync();

                return Results.Ok(200);
            }catch(Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
