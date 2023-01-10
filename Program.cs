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
app.MapGet("/api/tareas/prioridad/{prio}", async ([FromServices] TareasContext dbContext, [FromRoute] int prio) =>
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


// Endpoint para actualizar datos de una tarea
app.MapPut("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea, [FromRoute] Guid id) =>
{
    // Buscar la tarea en cuestion
    var tareaActual = dbContext.Tareas.Find(id);
    // Verificar que la tarea existe
    if(tareaActual != null)
    {
        // Se actualizan cada uno de los campos que se deben cambiar
        tareaActual.CategoriaId = tarea.CategoriaId;
        tareaActual.Titulo = tarea.Titulo;
        tareaActual.PrioridadTarea = tarea.PrioridadTarea;
        tareaActual.Description = tarea.Description;

        // Se guardan los cambios realizados
        await dbContext.SaveChangesAsync();

        // Respuesta Ok 200
        return Results.Ok();

    } else
    {
        return Results.NotFound();
    }
});

// Actualizar categorias
app.MapPut("/api/categorias/{id}", async ([FromServices] TareasContext dbContext, [FromBody] Categoria categoria, [FromRoute] Guid id) =>
{
    // Buscar la categoria en cuestion
    var categoriaActual = dbContext.Categorias.Find(id);
    // Verificar que la categoria existe
    if(categoriaActual != null)
    {
        categoriaActual.Nombre = categoria.Nombre;
        categoriaActual.Description = categoria.Description;
        categoriaActual.Peso = categoria.Peso;

        // Se guardan los cambios realizados
        await dbContext.SaveChangesAsync();

        // Respuesta Ok 200
        return Results.Ok();

    } else
    {
        return Results.NotFound();
    }
    
    
});

app.Run();
