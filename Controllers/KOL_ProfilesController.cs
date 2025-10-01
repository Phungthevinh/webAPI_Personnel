//using Npgsql;
//using WebAPI.models;

//namespace WebAPI.Controllers
//{
//    public class KOL_ProfilesController
//    {
//        public async Task<string> ThemmoiprofileKOL(NpgsqlDataSource db, KOL_Profiles KOL)
//        {
//            await using var connection = await db.OpenConnectionAsync();
//            Console.WriteLine(KOL.Phone);
//            await using var cmd = new NpgsqlCommand("UPDATE KOL_Profiles Set phone = @p1 WHERE user_id = @p2", connection)
//            {
//                Parameters =
//                        {
//                            new("p1", KOL.Phone),
//                            new("p2", KOL.User_id)
//                        }
//            };

//            await cmd.ExecuteNonQueryAsync();

//            return "Thêm thành công";
//        }
//    }
//}
