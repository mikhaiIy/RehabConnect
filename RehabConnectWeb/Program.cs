using Microsoft.EntityFrameworkCore;
using RehabConnect.DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using RehabConnect.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Json.Serialization;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.DataAccess.Repository;
//using Stripe;
//using DinkToPdf;
//using DinkToPdf.Contracts;
//using RehabConnectWeb.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
  options.LoginPath = $"/Identity/Account/Login";
  options.LogoutPath = $"/Identity/Account/Logout";
  options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

//builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
//builder.Services.AddTransient<PdfService>();

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

//StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
  name: "admin",
  pattern: "{area=exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
  name: "parent",
  pattern: "{area=exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
  name: "therapist",
  pattern: "{area=exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
  name: "default",
  pattern: "{area=LandingPage}/{controller=FrontPages}/{action=LandingPage}/{id?}"
);

app.Run();
