using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using WebAPI.Controllers;
using WebAPI.models;

namespace WebAPI.routers
{
    public class UsersRouter
    {
        public UsersRouter(WebApplication app, NpgsqlDataSource db, string keyJWT, string Issuer, string Audience)
        {
            //đăng ký tài khoản cho KOL
            app.MapPost("/dangky", (Users user) =>
            {
                UsersControlers u = new UsersControlers();
                u.addUsers(user, db);
                return "Đăng ký thành công";
            });

            //đăng nhập cho người dùng
            app.MapPost("/dangNhap", (DangNhap dangnhap) =>
            {
                UsersControlers tokenHandler = new UsersControlers();
                return tokenHandler.dangnhap(dangnhap, keyJWT, db, Issuer, Audience);
            });

            app.MapGet("/test", [Authorize] () => "hello");
        }
    }
}
