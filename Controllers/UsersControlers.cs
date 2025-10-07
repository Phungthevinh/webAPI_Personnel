
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.models;
using WebAPI.Services;
using BCr = BCrypt.Net;


namespace WebAPI.Controllers
{
    public class UsersControlers
    {
        private readonly dbContext _dbContext; 
        public UsersControlers(dbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //đăng ký người dùng
        public async Task<IResult> addUsers(Users users)
        {
            try
            {
                string passwordHash = BCr.BCrypt.HashPassword(users.password_hash);
                DateTime now = DateTime.UtcNow;

                _dbContext.Add(new Users
                {
                    username = users.username,
                    password_hash = passwordHash,
                    full_name = users.full_name,
                    email = users.email,
                    created_at = now,
                    KOL_Profile = new List<KOL_Profiles>
                    {
                        new KOL_Profiles {},
                    }
                });
                await _dbContext.SaveChangesAsync();
                return Results.Ok(200);

            }
            catch (NpgsqlException ex)
            {
                // Lỗi từ cơ sở dữ liệu PostgreSQL
                Console.WriteLine($"Lỗi PostgreSQL: {ex.Message}");
                return Results.Ok(500);

            }

        }

        //đăng nhập người dùng
        public async Task<string> dangnhap(DangNhap dangnhap, string keyJWT, string Issuer, string Audience)
        {

            
            var user = await _dbContext.users
                .Where(u => u.username == dangnhap.Username)
                .FirstOrDefaultAsync();
 
            //đọc ra kết quả
            if (user == null)
            {
                return "userName không chính xác";
            }

            if (dangnhap.Password_hash == null || !BCr.BCrypt.Verify(dangnhap.Password_hash, user.password_hash))
            {
                return "Mật khẩu không chính xác";
            }
            //tạo JWT
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyJWT));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Email, user.email),
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = credentials,
                Issuer = Issuer,
                Audience = Audience
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(securityToken);
            return tokenString;
        }

        //thông tin người dùng sau khi đăng nhập
        public async Task<IResult> laythongtinnguoidung(ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
            {
                return Results.Unauthorized();
            }
            var userName = user.Identity.Name;
            
            var u = await _dbContext.users
                .Where(us => us.username == userName)
                .FirstOrDefaultAsync();
            return Results.Ok(new
            {
                _id = u.id,
                _username = u.username,
                _email = u.email,
                _name = u.full_name
            });
        }

        // tất cả người dùng và chức vụ của họ
        public async Task<IResult> tatCaNguoiDungVaThongTinCuaNguoiDung()
        {
            try
            {
                var nguoidungvaChucvu = await _dbContext.users
                    .OrderBy(us => us.id)
                    .Select(user => new
                    {
                        idnguoidung = user.id,
                        tennguoidung = user.full_name,
                        email = user.email,
                        hoatdong = user.is_active,
                        chucvu = user.user_roles.Select(ur => new {chucvu = ur.Roles.name})
                    })
                    .ToListAsync();
                return Results.Ok(nguoidungvaChucvu);
            }
            catch (Exception ex) { 
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
