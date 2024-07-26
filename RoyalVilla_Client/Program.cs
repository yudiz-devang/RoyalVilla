using Blazored.LocalStorage;
using HiddenVilla_Client.Service;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RoyalVilla_Client;
using RoyalVilla_Client.Service;
using RoyalVilla_Client.Service.IService;
using RoyalVilla_Client.Helper;
using Stripe;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")) });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<IHotelAmenityService, HotelAmenityService>();
builder.Services.AddScoped<IRoomOrderDetailsService, RoomOrderDetailsService>();
builder.Services.AddScoped<IStripePaymentService, StripePaymentService>();
builder.Services.AddScoped<IHotelRoomService, HotelRoomService>();
builder.Services.AddScoped<IAuthenticationServices, AuthenticationService>();
//var provider = builder.Services.BuildServiceProvider();
//var configuration = provider.GetRequiredService<IConfiguration>();
//StripeConfiguration.ApiKey = configuration.GetSection("Stripe")["ApiKey"];



await builder.Build().RunAsync();
