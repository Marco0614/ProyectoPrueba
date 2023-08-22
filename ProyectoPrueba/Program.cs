using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoPrueba.Areas.Identity.Data;
using ProyectoPrueba.Data;
using ProyectoPrueba.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProyectoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexion") ?? throw new InvalidOperationException("Connection string 'EjercicioBDFirstContext' not found.")));


var connectionString = builder.Configuration.GetConnectionString("ProyectoPruebaContextConnection") ?? throw new InvalidOperationException("Connection string 'ProyectoPruebaContextConnection' not found.");

builder.Services.AddDbContext<ProyectoPruebaContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ProyectoPruebaUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ProyectoPruebaContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
