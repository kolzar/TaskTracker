using Dapper;
using SQLitePCL;
using TaskTracker.Api.Data;
using TaskTracker.Api.Infrastructure.Dapper;
using TaskTracker.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Initialize SQLite provider
Batteries.Init();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

SqlMapper.AddTypeHandler(new GuidTypeHandler());

var app = builder.Build();

var databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
databaseInitializer.Initialize();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();