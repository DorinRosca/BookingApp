using Booking.Application;
using Booking.Database;
using Booking.Domain.Entities;
using Booking.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.Configure<RazorViewEngineOptions>(o =>
{
     o.ViewLocationFormats.Clear();
     o.ViewLocationFormats.Add("/UI/{1}/{0}" + RazorViewEngine.ViewExtension);
     o.ViewLocationFormats.Add("/UI/Shared/{0}" + RazorViewEngine.ViewExtension);
});

builder.Services.AddRouting(options =>
{
     options.AppendTrailingSlash = true;
     options.LowercaseUrls = true;
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddLazyCache();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<BookingDbContext>();
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
