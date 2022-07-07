global using CivilizationAPI.Data;
global using Shared.Models;
global using Shared.Models.Civilization;
global using Shared.Models.FortuneWheel;


global using CivilizationAPI.Services.PlayerService;
global using CivilizationAPI.Services.PrizeService;
global using CivilizationAPI.Services.WheelService;
global using CivilizationAPI.Services.AuthService;
global using CivilizationAPI.Services.MissionService;
global using CivilizationAPI.Services.DistrictService;
global using CivilizationAPI.Services.EraService;
global using CivilizationAPI.Services.LoggingService;
global using CivilizationAPI.Services.UnitService;
global using CivilizationAPI.Services.MissionTypeService;

global using Microsoft.EntityFrameworkCore;

global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using System.Text.Json.Serialization;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Server.IISIntegration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(options => 
{
    options.DefaultAuthenticateScheme = IISDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
        .GetBytes(builder.Configuration["WheelAPIToken"]))
    };
});

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Local"));
});

builder.Services.AddScoped<IPrizeService, PrizeService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IWheelService, WheelService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMissionService, MissionService>();
builder.Services.AddScoped<IMissionTypeService, MissionTypeService>();
builder.Services.AddScoped<IEraService, EraService>();
builder.Services.AddScoped<IDistrictService, DistrictService>();
builder.Services.AddScoped<IUnitService, UnitService>();

builder.Services.AddScoped<ILoggingService, LoggingService>();

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true).AddNewtonsoftJson();

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standart Authorization Header using the Bearer Scheme (\"Bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}


app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
