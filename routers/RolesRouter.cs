using Microsoft.AspNetCore.Authorization;
using NpgsqlTypes;
using WebAPI.Controllers;
using WebAPI.models;
using WebAPI.Services;

namespace WebAPI.routers
{
    public class RolesRouter
    {
       public RolesRouter(WebApplication app)
        {
            //thêm mới vai trò
            app.MapPost("/them-moi-vai-tro", [Authorize] (Roles roles, dbContext db) =>
            {
                RolesController themmoivaitro = new RolesController(db);
                return themmoivaitro.themMoiRole(roles);
            });

            //xem tất cả vai trò
            app.MapGet("/tat-ca-vai-tro", [Authorize] (dbContext db) =>
            {
                RolesController xemtatcavaitro = new RolesController(db);
                return xemtatcavaitro.xemVaiTro();
            });

            //cập nhập vai trò
            //xóa vai trò
            app.MapDelete("xoa-vai-tro", (dbContext db, int id) =>
            {
                RolesController xoaVaiTro = new RolesController(db);
                return xoaVaiTro.xoaVaiTro(id);
            });

            //thêm người dùng vào vai trò tương ứng
            app.MapPost("/them-nguoi-dung-vao-vai-tro", [Authorize] (dbContext db, user_roles user_role) =>
            {
                RolesController themnguoidungvaovaitro = new RolesController(db);
                return themnguoidungvaovaitro.themnguoidungvaovaitro(user_role);
            });

            //xóa vai trò người dùng
            app.MapDelete("/xoa-vai-tro-nguoi-dung", [Authorize] (dbContext db, int user_id) =>
            {
                RolesController xoaVaiTroNguoiDung = new RolesController(db);
                return xoaVaiTroNguoiDung.xoaVaiTroNguoiDung(user_id);
            });
        }
    }
}
