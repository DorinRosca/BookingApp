using CarBookingApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using CarBookingApp.Interfaces;
using CarBookingApp.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IDrive, DriveRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(options =>
{
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
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
     name: "Create",
     pattern: "Drive/Create",
     defaults: new { controller = "Drive", action = "Create" }
);

app.MapControllerRoute(
     name: "Delete",
     pattern: "Drive/Delete/{id}",
     defaults: new { controller = "Drive", action = "Delete" }
);

app.MapControllerRoute(
     name: "default",
     pattern: "{controller=Home}/{action=Index}/{id?}"
);


app.Run();
