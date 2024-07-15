using Core.Domains;
using Core.Validations;
using Data.Context;
using Data.DTOs.Book;
using Data.DTOs.UserDTO;
using Data.Validations;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Service.BookServices;
using Service.CategoryServices;
using Service.FrenidShipServices;
using Service.Image;
using Service.ReviewServices;
using Service.SectionServices;
using Service.ShelfServices;
using Service.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add SignalR services
builder.Services.AddSignalR();

// add database
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISectionService, SectionService>();
builder.Services.AddScoped<IShelfService, ShelfService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<IFreindShipService, FreindShipService>();
builder.Services.AddScoped<IValidator<BookDTO>, BookValidator>();
builder.Services.AddScoped<IValidator<UserDTO>, UserValidator>();
builder.Services.AddSwaggerGen(x=> x.SwaggerDoc("v1",new OpenApiInfo { Title = "LibraryApi",Version = "v1" }));

builder.Services.AddIdentity<ApplicationUser, AppRole>(opt =>
{
    opt.Password.RequiredLength = 7;
    opt.Password.RequireDigit = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json","LibraryApi v1"));
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json","LibraryApi v1"));

app.Run();
