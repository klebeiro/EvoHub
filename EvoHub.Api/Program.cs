using EvoHub.Business.Contract;
using EvoHub.Business.Services;
using EvoHub.Domain.Interfaces;
using EvoHub.Infra.ApiGitHub;
using EvoHub.Infra.Contract;
using EvoHub.Infra.Database;
using EvoHub.Infra.Middlewares;
using EvoHub.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfiguration configuration = builder.Configuration;
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<RestClient>();
builder.Services.AddScoped<IGitHubApi, GitHubApi>();
builder.Services.AddScoped<IGitHubApiBusiness, GitHubApiBusiness>();
builder.Services.AddScoped<IFavoritesRepository, FavoritesRepository>();
builder.Services.AddScoped<IFavoritesBusiness, FavoritesBusiness>();
builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("SQLite")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware(typeof(GlobalExceptionHandling));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
