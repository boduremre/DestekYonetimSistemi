using DestekYonetimSistemi;
using DestekYonetimSistemi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Ba�lant� dizesini al ve DbContext'i yap�land�r
builder.Services.AddDbContext<SupportManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Asp.Net Identity'i yap�land�r
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<SupportManagementDbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT ayarlar� i�in secret key
var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtConfig:Secret"]);

// JWT authentication ekle
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false, // E�er issuer'� kontrol etmek istersen ayarla
        ValidateAudience = false, // E�er audience'� kontrol etmek istersen ayarla
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Controller ekle
builder.Services.AddControllers();

var app = builder.Build();

// Veritaban�n� ba�lat
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await AppDbInitializer.InitializeAsync(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
