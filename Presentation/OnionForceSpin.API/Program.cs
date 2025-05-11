using OnionForceSpin.Persistence;
using OnionForceSpin.Application;
using OnionForceSpin.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Controller'larý ekledik
builder.Services.AddEndpointsApiExplorer(); // Swagger için minimal API'ler
builder.Services.AddSwaggerGen(); // Swagger için controller destekli jenerasyon

builder.Services.AddPersistence(builder.Configuration); // Kendi katmanýmýz
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

app.UseAuthorization(); // Eðer yetkilendirme varsa ekle

app.MapControllers(); // << Controller route'larýný aktif eder

app.Run();
