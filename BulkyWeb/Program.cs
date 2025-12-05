using Bulky.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

/* Dependency Injection Lifetime
    builder.Services.AddTransient<>();  // New service - every time it is requested
    builder.Services.AddScoped<>();     // New service - once per request
    builder.Services.AddSingleton<>();  // New service - once per application lifetime
 */

// Add/configure entity framework core and adds to dependency injection so you don't have to handle creating/closing DbContext on each page.
// Tools > NuGet Package manager > Package Manager Core
// Enter "update-database" to create database
builder.Services.AddDbContext<ApplicationDbContext>(options=>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// Pattern is after domain with the default controller as home and action is index
// id is null if not explicit
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Category}/{action=Index}/{id?}");

app.Run();
