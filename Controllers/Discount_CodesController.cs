using Npgsql;
using WebAPI.models;

namespace WebAPI.Controllers
{
    public class Discount_CodesController
    {
        //thêm mới mã giảm giá
        public async Task<string> themmoimagiamgia(Discount_Codes discount_Codes, NpgsqlDataSource db)
        {
            await using var connection = await db.OpenConnectionAsync();
            await using var cmd = new NpgsqlCommand("INSERT INTO Discount_Codes (code, kol_id, discount_value) VALUES ($1, $2, $3)", connection)
                {
                    Parameters =
                         {
                            new() { Value = discount_Codes.Code },
                            new() { Value = discount_Codes.Kol_id },
                            new() { Value = discount_Codes.Discount_value }
                         }
            };
            await cmd.ExecuteNonQueryAsync();
            return "thêm thành công";
        }

        //kích hoạt mã giảm giá
        public async Task<string> kichhoatmagiamgia(Discount_Codes discount_Codes, NpgsqlDataSource db)
        {
            await using var connection = await db.OpenConnectionAsync();
            discount_Codes.Activate();
            await using var cmd = new NpgsqlCommand("UPDATE Discount_Codes SET is_active = $1 Where code = $2;", connection)
            {
                Parameters =
                         {
                            new() { Value = discount_Codes.Is_active },
                            new() { Value = discount_Codes.Code },
                         }
            };
            await cmd.ExecuteNonQueryAsync();
            return "kích hoạt thành công";
        }

        //khóa mã giảm giá
        public async Task<string> khoamagiamgia(Discount_Codes discount_Codes, NpgsqlDataSource db)
        {
            await using var connection = await db.OpenConnectionAsync();
            discount_Codes.Deactivate();
            await using var cmd = new NpgsqlCommand("UPDATE Discount_Codes SET is_active = $1 Where code = $2;", connection)
            {
                Parameters =
                         {
                            new() { Value = discount_Codes.Is_active },
                            new() { Value = discount_Codes.Code },
                         }
            };
            await cmd.ExecuteNonQueryAsync();
            return "khóa thành công";
        }
    }
}
