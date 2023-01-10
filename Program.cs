using EFProject;
using EFProject.Models;
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
    return Results.Ok(dbContext.Tareas.Include(p => p.Categoria));
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

// Endpoint para agregar una nueva tarea
app.MapPost("/api/tareas", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea) =>
{
    // Se establecen datos que pueden no venir de parte del usuario
    tarea.TareaId = Guid.NewGuid();
    tarea.FechaCreacion = DateTime.Now;

    // Una vez construido nuestro objeto lo agregamos, esto siempre de manera asincrona
    //await dbContext.AddAsync(tarea);
    await dbContext.Tareas.AddAsync(tarea);
    // Se guardan los cambios
    await dbContext.SaveChangesAsync();

    // Respuesta Ok 200
    return Results.Ok();
});


app.Run();
