using OnionForceSpin.Persistence;
using OnionForceSpin.Application;
using OnionForceSpin.Mapper;
using OnionForceSpin.Application.Exceptions;
using MediatR;
using FluentValidation;
using OnionForceSpin.Application.CustomMiddlewares;
using OnionForceSpin.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;

builder.Configuration
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddCustomMapper();

//// 🔥 CORS EKLENİYOR
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//    {
//        policy.WithOrigins("http://192.168.43.135:5000", "https://192.168.43.135:5001")
//              .AllowAnyHeader()
//              .AllowAnyMethod();
//    });
//});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(System.Net.IPAddress.Any, 5000); // HTTP
    options.Listen(System.Net.IPAddress.Any, 5001, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "OnionForceSpin API",
        Version = "v1",
        Description = "OnionForceSpin API Documentation"
    });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme() {Name="Authorization",
       Type=SecuritySchemeType.ApiKey,
        Scheme="Bearer",
        BearerFormat="JWT",
        In=ParameterLocation.Header,
        Description= "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestLoggerMiddleware>();
app.ConfigureExceptionHandlingMiddleware();

// 🌐 CORS POLİTİKASI UYGULANIYOR — İsim doğru olmalı!
//app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();
app.Run();


//var builder = WebApplication.CreateBuilder(args);


//// Add services to the container.
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddPersistence(builder.Configuration);
//builder.Services.AddApplication();
//builder.Services.AddCustomMapper();
//builder.Services.AddExceptionHandling(); // 🔍 Middleware ekleniyor
//builder.Services.AddHttpContextAccessor(); // ⬅️ Bunu ekle!

//builder.Services.AddValidatorsFromAssemblyContaining<Program>(); // FluentValidation'ları ekle
////🔥 MediatR Pipeline Behavior Ekleniyor
//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(OnionForceSpin.Application.Behaviors.ErrorHandlingBehavior<,>));
//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(OnionForceSpin.Application.Behaviors.FluenValidationBehavior<,>));





//var app = builder.Build();

//// Middleware tetikleniyor
//app.UseExceptionHandling();

//// Swagger ayarları (Geliştirme modunda aktif)
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();
//app.Run();
