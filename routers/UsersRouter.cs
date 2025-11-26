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

            // lấy ra số lượng sử dụng mã giảm giá trong tháng và doanh thu mang về trong tháng
            app.MapGet("/doanh-thu-KOC", [Authorize] (dbContext dbContext, ClaimsPrincipal claims, DateTime date) =>
            {
                KOL_ProfilesController kOL_ProfilesController = new KOL_ProfilesController(dbContext);
                return kOL_ProfilesController.CouponReportsController(claims, date);
            });


            app.MapGet("/", () => "xin chao");
        }
    }
}
