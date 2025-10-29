using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using OpenAI.Chat;
using System.Reflection.Emit;
using System.Text;
using WebAPI.models;
using WebAPI.routers;
using WebAPI.Services;

DotNetEnv.Env.Load();

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Lấy config từ appsettings.json hoặc env
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var key = builder.Configuration["Jwt:Key"];
var issuer = builder.Configuration["Jwt:Issuer"];
var audience = builder.Configuration["Jwt:Audience"];


// Đăng ký NpgsqlDataSource vào DI container
await using var dataSource = NpgsqlDataSource.Create(connectionString);

builder.Services.AddDbContext<dbContext>(options =>
    options.UseNpgsql(connectionString));

//đăng ký ủy quyền vai trò vào danh tính
// Toàn bộ chuỗi lệnh là MỘT câu lệnh, kết thúc bằng dấu ; duy nhất
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<dbContext>(); // <--- Chuỗi lệnh hoàn chỉnh


//khởi tạo model GPT
builder.Services.AddSingleton<ChatClient>(serviceProvider =>
{
    var apiKey = System.Environment.GetEnvironmentVariable("OPEN_AI_KEY");
    
    var model = "gpt-4.1";
    return new ChatClient(model, apiKey);
});

// Cấu hình xác thực JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();

// Inject dataSource từ DI thay vì tạo tay

routers router = new routers(app, dataSource, key, issuer, audience);

app.Run();
