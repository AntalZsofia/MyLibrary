using Microsoft.EntityFrameworkCore;
using MyLibrary.Server.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Entity Framework DbContext Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ;
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("Server=localhost;Database=MyLibrary;User id=postgres;Password=0Kakipisifing00"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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