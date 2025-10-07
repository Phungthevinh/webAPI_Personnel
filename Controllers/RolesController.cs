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
        public async Task<IResult> xoaVaiTro(int id)
        {
            try
            {
                var delRole = await _dbContext.roles
                    .Where(r => r.id == id)
                    .FirstOrDefaultAsync();

                _dbContext.Remove(delRole);
                await _dbContext.SaveChangesAsync();

                return Results.Ok("xóa thành công");
            }catch(Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
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
        public async Task<IResult> xoaVaiTroNguoiDung(int user_id)
        {
            try
            {
                var user_rol = await _dbContext.user_roles
                    .Where(u => u.user_id == user_id)
                    .FirstOrDefaultAsync();

                _dbContext.Remove(user_rol);

                await _dbContext.SaveChangesAsync();
                return Results.Ok(200);

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }
        }
        //xóa vai trò người dùng
    }

    
}
