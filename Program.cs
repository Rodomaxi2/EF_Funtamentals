using EFProject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<TareasContext>( p => p.UseInMemoryDatabase("TareasDB"));

builder.Services.AddSqlServer<TareasContext>("Data Source=(local); Initial Catalog=TareasDb;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True");

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconn", async ([FromServices] TareasContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());
});

app.Run();
