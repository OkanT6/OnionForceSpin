using OnionForceSpin.Persistence;
using OnionForceSpin.Application;
using OnionForceSpin.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Controller'lar� ekledik
builder.Services.AddEndpointsApiExplorer(); // Swagger i�in minimal API'ler
builder.Services.AddSwaggerGen(); // Swagger i�in controller destekli jenerasyon

builder.Services.AddPersistence(builder.Configuration); // Kendi katman�m�z
builder.Services.AddApplication();
builder.Services.AddCustomMapper();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization(); // E�er yetkilendirme varsa ekle

app.MapControllers(); // << Controller route'lar�n� aktif eder

app.Run();
