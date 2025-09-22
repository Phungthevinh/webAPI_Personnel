using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.models;
using BCr = BCrypt.Net;


namespace WebAPI.Controllers
{
    public class UsersControlers
    {
        //đăng ký người dùng
        public async Task addUsers(Users user, NpgsqlDataSource db)
        {
            try
            {
                string passwordHash = BCr.BCrypt.HashPassword(user.PasswordHash);
                user.PasswordHash = passwordHash;
                await using var connection = await db.OpenConnectionAsync();
                await using var cmd = new NpgsqlCommand("INSERT INTO users (username, email, password_hash, full_name) VALUES ($1, $2, $3, $4) RETURNING id", connection)
                {
                    Parameters =
                {
                    new() {Value = user.Username},
                    new() {Value = user.Email},
                    new() {Value = user.PasswordHash},
                    new() {Value = user.FullName}
                }

                };

                var result = await cmd.ExecuteScalarAsync();
                await using var cmd1 = new NpgsqlCommand("INSERT INTO KOL_Profiles (user_id) VALUES ($1)", connection)
                {
                    Parameters =
                {
                    new() {Value = result}
                }
                };
                await cmd1.ExecuteNonQueryAsync();

            }
            catch (NpgsqlException ex)
            {
                // Lỗi từ cơ sở dữ liệu PostgreSQL
                Console.WriteLine($"Lỗi PostgreSQL: {ex.Message}");
                
            }
            
        }

        //đăng nhập người dùng
        public async Task<string> dangnhap(DangNhap dangnhap, string keyJWT, NpgsqlDataSource db, string Issuer, string Audience)
        {
            //kết nối csdl
            await using var connection = await db.OpenConnectionAsync();
            //truy vấn csdl
            await using var command = new NpgsqlCommand("select * from users where username = ($1)", connection)
            {
                Parameters =
                {
                    new() {Value = dangnhap.Username},
                }
            };
            //đọc ra kết quả
            await using var reader = await command.ExecuteReaderAsync();
            if (!await reader.ReadAsync()) {
                return "userName không chính xác";
            }

            var name = reader["username"].ToString();   
            var email = reader["email"].ToString();
            var passWord = reader["password_hash"].ToString();

            if (dangnhap.Password_hash == null || !BCr.BCrypt.Verify(dangnhap.Password_hash, passWord))
            {
                return "Mật khẩu không chính xác";
            }
            //tạo JWT
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyJWT));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email),
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

        public async Task<IResult> laythongtinnguoidung(ClaimsPrincipal user, NpgsqlDataSource db)
        {
            if (!user.Identity.IsAuthenticated)
            {
                return Results.Unauthorized();
            }
            var userName = user.Identity.Name;
            await using var connection = await db.OpenConnectionAsync();
            await using var command = new NpgsqlCommand("SELECT id, username, email, full_name from Users where username = $1", connection) 
            {
                Parameters =
                {
                    new() { Value = userName },
                    
                }
            };
            await using var result = await command.ExecuteReaderAsync();
            if (!await result.ReadAsync())
            {
                
                return Results.Unauthorized();
            }
            var id = result.GetInt32(0);
            var username = result.GetString(1);
            var email = result.GetString(2);
            var name = result.GetString(3);

            return Results.Ok(new
            {
                _id = id,
                _username = username,
                _email = email,
                _name = name
            });
        }
    }
}
