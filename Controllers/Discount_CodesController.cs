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
                Console.WriteLine(discount_Codes.is_active);
                _db.Add(new discount_codes
                {
                    code = discount_Codes.code,
                    kol_id = discount_Codes.kol_id,
                    discount_value = discount_Codes.discount_value,
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
        //public async Task<string> kichhoatmagiamgia(Discount_Codes discount_Codes, NpgsqlDataSource db)
        //{
        //    await using var connection = await db.OpenConnectionAsync();
        //    discount_Codes.Activate();
        //    await using var cmd = new NpgsqlCommand("UPDATE Discount_Codes SET is_active = $1 Where code = $2;", connection)
        //    {
        //        Parameters =
        //                 {
        //                    new() { Value = discount_Codes.Is_active },
        //                    new() { Value = discount_Codes.Code },
        //                 }
        //    };
        //    await cmd.ExecuteNonQueryAsync();
        //    return "kích hoạt thành công";
        //}

        //khóa mã giảm giá
        //public async Task<string> khoamagiamgia(Discount_Codes discount_Codes, NpgsqlDataSource db)
        //{
        //    await using var connection = await db.OpenConnectionAsync();
        //    discount_Codes.Deactivate();
        //    await using var cmd = new NpgsqlCommand("UPDATE Discount_Codes SET is_active = $1 Where code = $2;", connection)
        //    {
        //        Parameters =
        //                 {
        //                    new() { Value = discount_Codes.Is_active },
        //                    new() { Value = discount_Codes.Code },
        //                 }
        //    };
        //    await cmd.ExecuteNonQueryAsync();
        //    return "khóa thành công";
        //}
    }
}
