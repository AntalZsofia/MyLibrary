using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyLib.Models;
using MyLib.Models.Entities;
using MyLib.Services;

var builder = WebApplication.CreateBuilder(args);
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var configuration = builder.Configuration;

// Apply cors policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy  =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IDiscussionService, DiscussionService>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<IdentityOptions>(options => options.SignIn.RequireConfirmedEmail = false);

//Add Api key
 // IConfiguration configurationA = new ConfigurationBuilder()
 //     .SetBasePath(Directory.GetCurrentDirectory())
 //     .AddJsonFile("appsettings.json") 
 //     .Build();
 //
 // string? apiKey = configurationA.GetSection("GoogleBooksApi")["ApiKey"];


builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.Cookie.Name = "MyAuthCookie";
    })
    .AddJwtBearer(
        options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:ValidIssuer"],
                ValidAudience = configuration["JWT:ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTAuthenticationHighlySecuredSecretKey12345"))
            };
            options.Events = new JwtBearerEvents()
            {
                OnMessageReceived = context =>
                {
                    Console.WriteLine(context.Request.Cookies["token"]);
                    context.Token = context.Request.Cookies["token"];
                    return Task.CompletedTask;
                }
            };
        });

// Entity Framework DbContext Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ;
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("Server=localhost;Database=MyLib;User id=postgres;Password=0Kakipisifing00"));



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
    var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
    RoleManager<IdentityRole<Guid>>? roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole<Guid>>>();
    await ApplicationDbContext.Seed(db!, userManager!, roleManager!);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();