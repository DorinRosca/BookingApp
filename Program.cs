using CarBookingApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using Microsoft.CodeAnalysis.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddAuthorization(options =>
{
     options.AddPolicy("Admin",
          policy => policy.RequireRole("Admin"));
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
     options.ViewLocationFormats.Clear();
     // Add the root path where your custom views are located
     options.ViewLocationFormats.Add("/Features/Shared/{0}" + RazorViewEngine.ViewExtension);
     options.ViewLocationFormats.Add("/Features/{1}/{0}" + RazorViewEngine.ViewExtension);

});



builder.Services.AddDbContext<DataContext>(options =>
{
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddLazyCache();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DataContext>();
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
     pattern: "{controller=Cars}/{action=Index}/{id?}"
);


app.Run();
