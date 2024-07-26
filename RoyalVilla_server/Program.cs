using Business.Repository.IRepository;
using Business.Repository;
using DataAcess.Data;
using Microsoft.EntityFrameworkCore;
using RoyalVilla_server.Data;
using RoyalVilla_server.Service;
using RoyalVilla_server.Service.IService;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Blazored.Toast;
using DataAcesss.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IHotelRoomRepository, HotelRoomRepository>();
builder.Services.AddScoped<IHotelImageRepository, HotelImageRepository>();
builder.Services.AddScoped<IAmenityRepository, AmenityRepository>();
builder.Services.AddScoped<IRoomOrderDetailsRepository, RoomOrderDetailsRepository>();
builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddScoped<IDBInitializer, DbInitializer>();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddBlazoredToast();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connString));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders().AddDefaultUI();
var app = builder.Build();

var scope = app.Services.CreateScope();
var service = scope.ServiceProvider.GetService<IDBInitializer>();
service.Initialize();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection(); 
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
