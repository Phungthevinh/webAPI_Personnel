using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using OpenAI.Chat;
using System.Text;
using WebAPI.routers;

DotNetEnv.Env.Load();

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Lấy config từ appsettings.json hoặc env
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var key = builder.Configuration["Jwt:Key"];
var issuer = builder.Configuration["Jwt:Issuer"];
var audience = builder.Configuration["Jwt:Audience"];

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("https://chatdt.netlify.app",
                                              "http://www.contoso.com")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

// Đăng ký NpgsqlDataSource vào DI container
await using var dataSource = NpgsqlDataSource.Create(connectionString);

//khởi tạo model GPT
builder.Services.AddSingleton<ChatClient>(serviceProvider =>
{
    var apiKey = System.Environment.GetEnvironmentVariable("OPEN_AI_KEY"); ;
    
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
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

// Inject dataSource từ DI thay vì tạo tay

routers router = new routers(app, dataSource, key, issuer, audience);

app.Run();
