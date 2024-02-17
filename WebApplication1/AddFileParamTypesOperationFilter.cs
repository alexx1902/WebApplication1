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
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApplication1.Controller;

public class AddFileParamTypesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.ParameterDescriptions.Any(p => p.ModelMetadata.ContainerType == typeof(FileUploadController)))
        {
            var schema = new OpenApiSchema
            {
                Type = "string",
                Format = "binary"
            };
            operation.RequestBody = new OpenApiRequestBody
            {
                Content =
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = schema
                    }
                }
            };
        }
    }
}