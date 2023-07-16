using CarBookingApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using CarBookingApp.Interfaces;
using CarBookingApp.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IAdmin, AdminRepository>();
builder.Services.AddScoped<ICar, CarRepository>();
builder.Services.AddScoped<IOrder, OrderRepository>();
builder.Services.AddScoped<IRole, RoleRepository>();
builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAuthorization(options =>
{
     options.AddPolicy("Admin",
          policy => policy.RequireRole("Admin"));
});
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
     name: "default",
     pattern: "{controller=Cars}/{action=Index}/{id?}"
);


app.Run();
