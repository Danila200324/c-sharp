

using Zadanie8.Models;
using Zadanie8.Services;
using Zadanie8.Services.ServicesIml;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IPrescriptionService, PrescriptionServiceImpl>();
builder.Services.AddTransient<IDoctorService, DoctorServiceImpl>();
// Add services to the container.
builder.Services.AddDbContext<MainDbContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
