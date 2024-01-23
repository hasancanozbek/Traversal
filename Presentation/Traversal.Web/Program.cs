using DataAccessLayer.EntityFrameworkCore;
using EntityLayer.Concretes;
using Microsoft.AspNetCore.Identity;
using Traversal.Web.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddBusinessServices();
builder.Services.AddDataAccessServices();
builder.Services.AddCoreServices();
builder.Services.AddIdentity<User, UserRole>().AddEntityFrameworkStores<TraversalDbContext>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>,
    CustomClaimsPrincipalFactory>();

var app = builder.Build();

app.UseAuthentication();
// Configure the HTTP request pipeline.
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
