using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListApiRecreate_1.Controllers;
using TodoListApiRecreate_1.Data;
using TodoListApiRecreate_1.Dto;
using TodoListApiRecreate_1.Models;
using TodoListApiRecreate_1.Services; // anter pga jeg ikke har en fil i service derfor den mener mappen ikke eksiser.


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(// har en feil her antar det er pga mangler using Npgsql somehere må finne ut why next.
        builder.Configuration.GetConnectionString("DefaultConection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
