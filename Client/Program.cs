global using Shared.Models;
global using Shared.Models.Civilization;
global using Shared.Models.FortuneWheel;

global using Client.Services.WheelService;
global using Client.Services.DistrictService;
global using Client.Services.UnitService;
global using Client.Services.EraService;
global using Client.Services.MissionService;
global using Client.Services.MissionTypeService;
global using Client.Services.PlayerService;
global using Client.Services.PrizeService;
global using Client.Services.AuthService;

using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Blazored.Modal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddBlazoredModal();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMvc();


builder.Services.AddScoped(sp => new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true })
{ 
    BaseAddress = new Uri(builder.Configuration["APIAddress"]) 
});

builder.Services.AddScoped<IPrizeService, PrizeService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IWheelService, WheelService>();
builder.Services.AddScoped<IMissionService, MissionService>();
builder.Services.AddScoped<IMissionTypeService, MissionTypeService>();
builder.Services.AddScoped<IEraService, EraService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IDistrictService, DistrictService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

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
