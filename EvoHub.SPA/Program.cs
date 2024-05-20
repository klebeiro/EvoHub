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

IConfiguration configuration = builder.Configuration;
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<RestClient>();
builder.Services.AddScoped<IGitHubApi, GitHubApi>();
builder.Services.AddScoped<IGitHubApiBusiness, GitHubApiBusiness>();
builder.Services.AddScoped<IFavoritesRepository, FavoritesRepository>();
builder.Services.AddScoped<IFavoritesBusiness, FavoritesBusiness>();
builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("SQLite")));
builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod())
);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseMiddleware(typeof(GlobalExceptionHandling));

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
