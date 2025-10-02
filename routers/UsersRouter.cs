using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.Security.Claims;
using WebAPI.Controllers;
using WebAPI.models;
using WebAPI.Services;

namespace WebAPI.routers
{
    public class UsersRouter
    {
        public UsersRouter(WebApplication app, string keyJWT, string Issuer, string Audience)
        {
            //đăng ký tài khoản cho KOL
            app.MapPost("/dangky", (Users user, dbContext dbContext) =>
            {
                UsersControlers u = new UsersControlers(dbContext);
                return u.addUsers(user);
                
            });

            //đăng nhập cho người dùng
            app.MapPost("/dangNhap", (DangNhap dangnhap, dbContext dbContext) =>
            {
                UsersControlers tokenHandler = new UsersControlers(dbContext);
                return tokenHandler.dangnhap(dangnhap, keyJWT, Issuer, Audience);
            });

            //lấy ra thông tin sau khi đăng nhập
            app.MapGet("/thong-tin-sau-khi-dang-nhap", [Authorize] (ClaimsPrincipal user, dbContext dbContext) =>
            {
                UsersControlers tokenHandler = new UsersControlers(dbContext);
                return tokenHandler.laythongtinnguoidung(user);
            });

            app.MapGet("/test", () => "xin chao");
        }
    }
}
