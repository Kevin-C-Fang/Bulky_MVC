using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Bulky.Utility;
using Stripe;

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

// Automatically inject the app settings section key/value into the properties in StripeSettings as long as the naming matches.
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

// Adds identity to service, adds all the database tables needed for identity, and that they will be managed using ApplicationDbContext 
// builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();

// To add role to identity, you have to customize it by adding identity with the role.
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

// Configurations that automatically redirect the options.xxxx to the specified routing path, must be added after adding identity.
// This is due to those pages being within IdentityArea, since we don't want to copy the same files everywhere, just reference the path needed.
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

// Need to add and map razor pages for identity since it uses Razor pages and not MVC.
builder.Services.AddRazorPages();

// Register the IUnitOfWork to service so it can be dependency injected in CategoryController.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

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

// Configure stripe within project by setting the API key.
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseRouting();

// Checks if user credentials are valid
app.UseAuthentication();

// Allows access to website based on user role.
app.UseAuthorization();

app.MapRazorPages();

// Pattern is after domain with the default controller as home and action is index
// id is null if not explicit
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
