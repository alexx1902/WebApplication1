/*
using Microsoft.EntityFrameworkCore;
using WebApplication1;
using WebApplication1.Interfaces;
using WebApplication1.Repository;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IValueRepository, ValueRepository>();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString(nameof(DBContext)));
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CSV File API V1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

  
*/
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Threading.Tasks;
using WebApplication1;
using WebApplication1.Interfaces;
using WebApplication1.Repository;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IValueRepository, ValueRepository>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CSV File API", Version = "v1" });

    // Добавляем поддержку загрузки файлов в Swagger
    c.OperationFilter<AddFileParamTypesOperationFilter>();
});
builder.Services.AddDbContext<DBContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString(nameof(DBContext)));
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CSV File API V1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

