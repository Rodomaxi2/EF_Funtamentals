using EFProject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<TareasContext>( p => p.UseInMemoryDatabase("TareasDB"));

builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("connTareas"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconn", async ([FromServices] TareasContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());
});

// Endpoint para obtener todas las tareas con sus categorias
app.MapGet("/api/tareas/", async ([FromServices] TareasContext dbContext) =>
{
    return Results.Ok(dbContext.Tareas.Include(p => p.Categoria)
    .Where(p => p.PrioridadTarea == EFProject.Models.Prioridad.Media));
});

// Endpoint para obtener todas las tareas que cumplan con la prioridad indicada
app.MapGet("/api/tareas/prioridad/{prio}", async ([FromServices] TareasContext dbContext, int prio) =>
{
    return Results.Ok(dbContext.Tareas.Include(p => p.Categoria)
    .Where(p => (int)p.PrioridadTarea == prio));
});

// Endpoint para obtener todas las tareas que cumplan con dicha categoria
app.MapGet("/api/tareas/categoria/{name}", async ([FromServices] TareasContext dbContext, string name) =>
{
    return Results.Ok(dbContext.Tareas.Include(p => p.Categoria)
    .Where(p => p.Categoria.Nombre == name));
});


app.Run();
