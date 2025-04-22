using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using BAL.Shared;
using MODEL;
using MODEL.DTOs;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text; // Ensure that AppSettings is in the correct namespace

var builder = WebApplication.CreateBuilder(args);
var appSettings = new AppSettings();
// Add services to the container.

builder.Services.AddControllers();
builder.Configuration.GetSection("AppSettings").Bind(appSettings);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
ServiceManager.SetServiceInfo(builder.Services, appSettings);
builder.Services.AddControllers();

// Swagger/OpenAPI setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
   .AddJwtBearer(options =>
   {
       options.SaveToken = true;
       options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
       options.RequireHttpsMetadata = true;
       options.TokenValidationParameters = new()
       {
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAL0zIKgOk+azCEuVZvrvtkgjRk3VcSq4 kDzbi51WD2xCUGNafzI8cmoY9KqFh7s1V7C6nw3/QbzvTytwYR/c5Q0CAwEAAQ==")),
           ValidateLifetime = true,
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidIssuer = "Allianz_DEV",
           ValidAudience = "Allianz_DEV"
       };
   });


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});


var app = builder.Build();





// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
