
using Zadanie7.DAL;
using Zadanie7.Services.IServiceImpl;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using System.Configuration;
using Pomelo.EntityFrameworkCore.MySql;
using Zadanie7.Services.IService;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<Apbd7Context>(opt =>
{
    opt.UseMySQL("server=localhost;uid=bestuser;password=bestuser;database=apbd7",
        mysqlOptions => mysqlOptions.EnableRetryOnFailure());
    opt.LogTo(Console.WriteLine);
}   
);

builder.Services.AddTransient<ITripService, ITripServiceImpl>();
builder.Services.AddTransient<IClientService, IClientServiceImpl>();

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
