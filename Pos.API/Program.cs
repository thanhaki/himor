using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pos.API.Application;
using Pos.API.Common;
using Pos.API.Common.HandleError;
using Pos.API.Infrastructure;
using Pos.API.Infrastructure.Persistence;
using System.Text;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// For Entity Framework
var configuration = builder.Configuration;
builder.Services.AddDbContext<DBPosContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));

// For Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<DBPosContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(configuration);
builder.Services.AddCors();
builder.Services.AddMvc();

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
});

builder.Services.AddTransient<ErrorHandlingMiddleware>();

string strScret = Utilities.GetString("JWT:Secret");
string audience = Utilities.GetString("JWT:ValidAudience");
string issuer = Utilities.GetString("JWT:ValidIssuer");
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(option =>
{
    option.SaveToken = true;
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidAudience = audience,
        ValidateIssuer = true,
        ValidIssuer = issuer,
        RequireExpirationTime = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(strScret))
    };
});
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo 
        { 
            Title = "HIPOS API", 
            Version = "v1" 
        });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        option.IncludeXmlComments(xmlPath);

    option.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="bearer"
                }
            },
            new string[]{}
        }
    });

});

string folderLog = Utilities.GetString("SerilogPath");

string filePathLogs = Path.Combine(Directory.GetCurrentDirectory() + folderLog);

string p = string.Format(@"{0}hipos_.txt", filePathLogs);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File(p, rollingInterval: RollingInterval.Day)
    .CreateLogger();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<JwtMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region STATIC FILE

#region image don vi
string pathFileDv = Utilities.GetString("UploadImageDonVi:PathFile");
string pathAccessDv = Utilities.GetString("UploadImageDonVi:PathAccess");

string filePathDv = Path.Combine(Directory.GetCurrentDirectory() + pathFileDv);

if (!Directory.Exists(filePathDv))
    Directory.CreateDirectory(filePathDv);

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(filePathDv),
    RequestPath = new PathString(pathAccessDv)
});
#endregion


#endregion
var urls = Utilities.GetStringArray("CorsWithOrigins");

app.UseCors(options =>
     options.WithOrigins(urls)
            .AllowAnyHeader()
            .AllowAnyMethod());

app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
