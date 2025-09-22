
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using Npgsql;
using System.Text;
using WebAPI.routers;





var builder = WebApplication.CreateBuilder(args);

//lấy ra các thuộc tính reong file appsetting.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var key = builder.Configuration["Jwt:Key"];
var IssuerIssuer = builder.Configuration["Jwt:Issuer"];
var Audience = builder.Configuration["Jwt:Audience"];

//kết nối với csdl
await using var dataSource = NpgsqlDataSource.Create(connectionString);

//cấu hình xác thực
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = IssuerIssuer,
            ValidAudience = Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });
builder.Services.AddAuthorization();

//khởi tạo ứng dụng
var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();

routers router = new routers(app, dataSource, key, IssuerIssuer, Audience);



app.Run();
