using InventoryManagement.Application.Commands;
using InventoryManagement.Application.Queries;
using InventoryManagement.Extensions;
using InventoryManagement.Middleware;
using InventoryManagement.Models;
using MediatR;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddDependencyInjections();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
