global using AutoMapper;
global using Microsoft.OpenApi.Models;
global using Microsoft.EntityFrameworkCore;
global using Swashbuckle.AspNetCore.Filters;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Authentication.JwtBearer;

global using hr_system_backend.Entities.Details;
global using hr_system_backend.Dtos;
global using hr_system_backend.Services.Response;
global using hr_system_backend.Entities.Database;
global using hr_system_backend.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(builder =>
  {
    builder.AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader();
  });
});
builder.Services.AddSwaggerGen(
  opt =>
  {
    opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
      Description = @"This is a user authorization.
      Pleaes assign bearer [token] to sign in and to be get allowed to all
      endpoints independently.",
      In = ParameterLocation.Header,
      Name = "Authorization",
      Type = SecuritySchemeType.ApiKey
    });
    opt.OperationFilter<SecurityRequirementsOperationFilter>();
  }
);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<HRSystemDbContext>(
  opt =>
  {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbUrl"));
  }
);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(
    opt =>
    {
      opt.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Token").Value))
      };
    }
  );
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
