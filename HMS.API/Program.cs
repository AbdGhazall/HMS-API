using System.Runtime.InteropServices;
using System.Text;
using HMS.API.Abstraction.Interfaces;
using HMS.API.Abstraction.Interfaces.Appointment;
using HMS.API.Abstraction.Interfaces.Billing;
using HMS.API.Abstraction.Interfaces.Doctor;
using HMS.API.Abstraction.Interfaces.MedicalRecord;
using HMS.API.Abstraction.Interfaces.Patient;
using HMS.API.Abstraction.Interfaces.User;
using HMS.API.Filters;
using HMS.API.Services.Services;
using HMS.API.Services.Services.Appointment;
using HMS.API.Services.Services.Billing;
using HMS.API.Services.Services.Doctor;
using HMS.API.Services.Services.MedicalRecord;
using HMS.API.Services.Services.Patient;
using HMS.API.Services.Services.User;
using HMS.DAL.DataAccess;
using HMS.DAL.DataAccess.Managers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.EventLog;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

#region Caching

builder.Services.AddMemoryCache();

#endregion Caching

#region logging

// Clear all providers for logging.
builder.Host.ConfigureLogging((hostingContext, logging) =>
{
    logging.ClearProviders();
    var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    // IMPORTANT: This needs to be added *before* configuration is loaded, this lets
    // the defaults be overridden by the configuration.
    if (isWindows)
    {
        // Default the EventLogLoggerProvider to warning or above
        logging.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Warning);
    }
    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    logging.AddConsole();
    logging.AddDebug();
    logging.AddEventSourceLogger();
    if (isWindows)
    {
        // Add the EventLogLoggerProvider on windows machines
        logging.AddEventLog();
    }
}).UseDefaultServiceProvider((context, options) =>
{
    var isDevelopment = context.HostingEnvironment.IsDevelopment();
    options.ValidateScopes = isDevelopment;
    options.ValidateOnBuild = isDevelopment;
});
// Add log 4 net as logger
builder.Logging.AddLog4Net();

#endregion logging

#region swagger

//to use swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "HMS API",
        Version = "v1",
        Description = "An API for managing HMS operations",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Abdullrahman Ghazal",
            Email = "abdullrahman.ghazal@gmail.com",
        }
    });
    // Order endpoints based on group names
    //options.TagActionsBy(api => new[] { api.GroupName });
    // Add JWT Authentication button to Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token"
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
            new string[] {}
        }
    });
});

#endregion swagger

#region EntityFramework

// EF
builder.Services.AddDbContext<ApplicationDbContext>(db =>
db.UseSqlServer(builder.Configuration.GetConnectionString("HMSConnectionString")));

#endregion EntityFramework

#region DependencyInjection

//add interface and implemintation to service container (Dependincy Injection)
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserDataManager, UserDataManager>();
builder.Services.AddScoped<IUserValidationService, UserValidationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDoctorDataManager, DoctorDataManager>();
builder.Services.AddScoped<IDoctorValidationService, DoctorValidationService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IPatientDataManager, PatientDataManager>();
builder.Services.AddScoped<IPatientValidationService, PatientValidationService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IAppointmentDataManager, AppointmentDataManager>();
builder.Services.AddScoped<IAppointmentValidationService, AppointmentValidationService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IMedicalRecordDataManager, MedicalRecordDataManager>();
builder.Services.AddScoped<IMedicalRecordValidationService, MedicalRecordValidationService>();
builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
builder.Services.AddScoped<IBillingDataManager, BillingDataManager>();
builder.Services.AddScoped<IBillingValidationService, BillingValidationService>();
builder.Services.AddScoped<IBillingService, BillingService>();

#endregion DependencyInjection

#region Filters

//for filters
builder.Services.AddControllers(options =>
{
    options.Filters.Add<BaseFilter>();
});

#endregion Filters

builder.Services.AddOpenApi();

#region JWT

//for JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

#endregion JWT

var app = builder.Build();

#region Swagger

//to use swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HMS API V1");
    c.RoutePrefix = string.Empty; // Serve Swagger UI at root
});

#endregion Swagger

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//authentication first then authorization!!
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();