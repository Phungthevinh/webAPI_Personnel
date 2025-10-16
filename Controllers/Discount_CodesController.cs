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
        //xem mã giảm giá tương ứng với sự kiện nào
        public async Task<IResult> maGiamGiaTuongUngSuKien(int kol_id)
        {
            try
            {
                var dsmagiamgia = from discount_code in _db.discount_codes
                                  where discount_code.kol_id == kol_id
                                  join campaign in _db.campaigns on discount_code.campaign_id equals campaign.id
                                  select new { nameEvent = campaign.name, codes = discount_code.code, start_day = campaign.start_date, end_day = campaign.end_date, discount_value = campaign.discount_value, commission_value = campaign.commission_value };
                return Results.Ok(dsmagiamgia);
            }catch(Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
