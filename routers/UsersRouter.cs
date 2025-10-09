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
            //đăng ký tài khoản cho KOC
            app.MapPost("/dangky", (role_user role_User, dbContext dbContext) =>
            {
                UsersControlers u = new UsersControlers(dbContext);
                return u.addUsers(role_User);
                
            });

            //đăng nhập cho người dùng
            app.MapPost("/dangNhap", (DangNhap dangnhap, dbContext dbContext) =>
            {
                UsersControlers tokenHandler = new UsersControlers(dbContext);
                return tokenHandler.dangnhap(dangnhap, keyJWT, Issuer, Audience);
            });

            //lấy ra thông tin sau khi đăng nhập có nghĩa là đăng nhập và sẽ trả về thông tin đăng nhập cho người đã đăng nhập
            app.MapGet("/thong-tin-sau-khi-dang-nhap", [Authorize] (ClaimsPrincipal user, dbContext dbContext) =>
            {
                UsersControlers tokenHandler = new UsersControlers(dbContext);
                return tokenHandler.laythongtinnguoidung(user);
            });

            //lấy ra tất cả thông tin người dùng kèm chức vụ
            app.MapGet("/thong-tin-nguoi-dung-kem-chuc-vu", [Authorize] (dbContext dbContext) =>
            {
                UsersControlers nguoiDungVaChucVu = new UsersControlers(dbContext);
                return nguoiDungVaChucVu.tatCaNguoiDungVaThongTinCuaNguoiDung();
            });

            //lấy ra mã giảm giá kèm KOC tương ứng với mã giảm giá đó
            app.MapGet("/ma-giam-gia-kem-KOC", (dbContext dbContext) =>
            {
                UsersControlers kocDiscount = new UsersControlers(dbContext);
                return kocDiscount.magiamgiavanguoidung();
            });

            app.MapGet("/test", () => "xin chao");
        }
    }
}
