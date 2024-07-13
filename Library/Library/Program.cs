using Core.Domains;
using Core.Validations;
using Data.Context;
using Data.DTOs.Book;
using Data.DTOs.UserDTO;
using Data.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Service.BookServices;
using Service.CategoryServices;
using Service.FrenidShipServices;
using Service.Hubs;
using Service.Image;
using Service.ReviewServices;
using Service.SectionServices;
using Service.ShelfServices;
using Service.User;

var builder = WebApplication.CreateBuilder(args);
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2U1hhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hTX5UdkZiWH1WcHxXQWBd");
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<AppDbContext>();

// Add SignalR services
builder.Services.AddSignalR();

// Builder Services
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<ISectionService, SectionService>();
builder.Services.AddScoped<IShelfService, ShelfService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFreindShipService, FreindShipService>();
builder.Services.AddScoped<IValidator<BookDTO>, BookValidator>();
builder.Services.AddScoped<IValidator<UserDTO>, UserValidator>();

builder.Services.AddIdentity<ApplicationUser, AppRole>(opt =>
{
    opt.Password.RequiredLength = 7;
    opt.Password.RequireDigit = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.SlidingExpiration = true;
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.LogoutPath = "/Account/Logout";
});



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
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.MapHub<NotificationHub>("/notificationHub");
app.MapControllers();
app.Run();
