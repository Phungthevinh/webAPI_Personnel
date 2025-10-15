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
        public async Task<IResult> themmoimagiamgiatheoSukien(discount_codes discount_Codes)
        {
            try
            {
                discount_Codes.Deactivate();
                var random = new System.Random();

                int randomNumber = random.Next(1000);

                var sukien = await _db.campaigns
                    .Where(c => c.id == discount_Codes.campaign_id)
                    .FirstOrDefaultAsync();

                string code = $"{sukien.name}{discount_Codes.kol_id*randomNumber}{sukien.discount_value}";
                Console.WriteLine(code);
                _db.Add(new discount_codes
                {
                    code = code,
                    kol_id = discount_Codes.kol_id,
                    campaign_id = discount_Codes.campaign_id,
                    is_active = discount_Codes.is_active
                });
                await _db.SaveChangesAsync();
                return Results.Ok();
            }catch(Exception ex)
            {
                return Results.BadRequest(ex.InnerException.Message);
            }


        }
    }
}
