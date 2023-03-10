using KardesAile.AspNetCoreHost.Authentication;
using KardesAile.AspNetCoreHost.Middlewares;
using KardesAile.Business.Context;
using KardesAile.Business.Implementations;
using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.Context;
using KardesAile.CommonTypes.Options;
using KardesAile.Database;
using KardesAile.Database.Abstracts;
using KardesAile.Database.Generators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using KardesAile.AspNetCoreHost;
using Microsoft.ApplicationInsights.Extensibility;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddHealthChecks();

builder.Services.AddScoped<IAuthenticationBusiness, AuthenticationBusiness>();
builder.Services.AddScoped<IAddressBusiness, AddressBusiness>();
builder.Services.AddScoped<ISupporterBusiness, SupporterBusiness>();
builder.Services.AddScoped<IDisasterVictimBusiness, DisasterVictimBusiness>();
builder.Services.AddScoped<IChildBusiness, ChildBusiness>();
builder.Services.AddScoped<IMatchingBusiness, MatchingBusiness>();
builder.Services.AddScoped<IModeratorBusiness, ModeratorBusiness>();
builder.Services.AddScoped<IAuditBusiness, AuditBusiness>();

builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddScoped<IAuditContext, AuditContext>();
builder.Services.AddScoped<IAuditColumnValuesGenerator, AuditColumnValuesGenerator>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddOptions<JwtOptions>()
    .BindConfiguration("Jwt")
    .ValidateDataAnnotations();

builder.Services.Configure<TokenValidationParameters>(options =>
    options.SetAuthenticationDefaults(builder.Configuration));

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters.SetAuthenticationDefaults(builder.Configuration));

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddAuthenticationSchemes(TokenHelpers.DefaultAuthenticationScheme)
        .Build();
});

builder.Services.AddDbContext<KardesAileDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"),
        optionsBuilder =>
        {
            optionsBuilder.MigrationsHistoryTable(KardesAileDbContext.MigrationHistoryTablename,
                KardesAileDbContext.SchemaName);
            optionsBuilder.MigrationsAssembly(typeof(KardesAileDbContext).Assembly.FullName);
        });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        corsPolicyBuilder =>
        {
            var origins = builder.Configuration.GetSection("Cors:AllowedUrls").Get<string[]>();
            corsPolicyBuilder.WithOrigins(origins)
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddOpenTelemetryTracingToApplicationInsights(builder.Configuration);

builder.Host
    .UseSerilog((builderContext, services, loggerConfiguration) =>
    {
        loggerConfiguration
            .Enrich.WithProperty("ApplicationName", builderContext.HostingEnvironment.ApplicationName)
            .Enrich.FromLogContext()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.PostgreSQL(
                connectionString: builderContext.Configuration.GetConnectionString("DbConnection"),
                tableName: "error_logs",
                needAutoCreateTable: true,
                schemaName: KardesAileDbContext.SchemaName,
                restrictedToMinimumLevel: LogEventLevel.Warning
            )
            .WriteTo.ApplicationInsights(services.GetRequiredService<TelemetryConfiguration>(), TelemetryConverter.Traces);
    });

builder.WebHost.CaptureStartupErrors(true);

var app = builder.Build();

app.MapHealthChecks("/health");

app.UseExceptionHandler(appError => { appError.Run(GlobalExceptionManager.Handler); });

app.MapGet("/", () => "API");

app.MapGet("{*path:regex(^robots\\w*.txt$)}", (string _) => "API");

// if (app.Environment.IsDevelopment())
    app.ApplyMigrations();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();