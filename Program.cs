using Microsoft.EntityFrameworkCore;
using Webapp1.Data;
using Webapp1.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Webapp1DbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Webapp1DbConnectionString")));

builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IInnmeldingerRepository, InnmeldingerRepository>();
builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
