using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
using WebAPI.models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class RolesController
    {
        private readonly dbContext _dbContext;
        public RolesController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //thêm mới vai trò
        public async Task<IResult> themMoiRole(Roles roles)
        {
            try
            {
                _dbContext.Add(new Roles
                {
                    name = roles.name,
                    description = roles.description,
                    created_at = DateTime.UtcNow
                });
                await _dbContext.SaveChangesAsync();
                return Results.Ok(200);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        //xem tất cả vai trò
        public async Task<IResult> xemVaiTro()
        {
            try
            {
                var role = await _dbContext.roles
                    .OrderBy(r => r.id)
                    .ToListAsync();

   
                return Results.Ok(new {vaiTro = role });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        //xóa vai trò
        //cập nhập vai trò

        //them người dung vao vai trò tương ứng
        public async Task<IResult> themnguoidungvaovaitro(user_roles user_Roles)
        {
            try
            {
                _dbContext.Add(new user_roles
                {
                    user_id = user_Roles.user_id,
                    role_id = user_Roles.role_id,
                });
                await _dbContext.SaveChangesAsync();
                return Results.Ok(200);
            }
            catch (Exception ex) { return Results.BadRequest(ex.InnerException?.Message ?? ex.Message); }
        }

        //chỉnh sửa vai trò người dùng
        //xóa vai trò người dùng
    }

    internal class User_roles
    {
        public long user_id { get; set; }
        public long role_id { get; set; }
    }
}
