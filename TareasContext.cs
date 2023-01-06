using Microsoft.EntityFrameworkCore;
using EFProject.Models;

namespace EFProject;

public class TareasContext: DbContext
{
    public DbSet<Categoria> Categorias {get;set;}
    public DbSet<Tarea> Tareas {get;set;}

    public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Datos semilla (iniciales) de Categoria
        List<Categoria> categoriasInit = new List<Categoria>();
        categoriasInit.Add(new Categoria() {CategoriaId = Guid.Parse("ed3c3bc7-7e9c-450f-b069-72dcd2712149"), Nombre = "Terminar cursos", Peso = 20 });
        categoriasInit.Add(new Categoria() {CategoriaId = Guid.Parse("ed3c3bc7-7e9c-450f-b069-72dcd2712145"), Nombre = "Escuela", Peso = 10 });


        // Fluent API para poder crear nuestras tabla Categoria con sus restricciones
        modelBuilder.Entity<Categoria>(categoria => 
        {
            categoria.ToTable("Categoria");
            categoria.HasKey(p => p.CategoriaId);

            categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);

            categoria.Property(p => p.Description).IsRequired(false);
            categoria.Property(p => p.Peso);

            // Agregamos los datos creados anteriormente con HasData
            categoria.HasData(categoriasInit);

        });

        // Datos semilla (iniciales) de Tarea
        List<Tarea> tareaInit = new List<Tarea>();
        tareaInit.Add(new Tarea() {TareaId = Guid.Parse("6661ba23-84e5-461c-aa2e-c5d12c97eaef"), CategoriaId = Guid.Parse("ed3c3bc7-7e9c-450f-b069-72dcd2712149"), PrioridadTarea = Prioridad.Media, Titulo = "Terminar curso de EF", FechaCreacion = DateTime.Now });
        tareaInit.Add(new Tarea() {TareaId = Guid.Parse("f6372f56-0b65-4550-8054-ec91070ba136"), CategoriaId = Guid.Parse("ed3c3bc7-7e9c-450f-b069-72dcd2712145"), PrioridadTarea = Prioridad.Alta, Titulo = "Recoger titulo fisico", FechaCreacion = DateTime.Now });


        // Fluent API para poder crear nuestras tabla Tarea con sus restricciones
        modelBuilder.Entity<Tarea>(tarea => {
            tarea.ToTable("Tarea");
            tarea.HasKey(t => t.TareaId);
            // Especificacion de llave foranea
            tarea.HasOne(t => t.Categoria).WithMany(t => t.Tareas).HasForeignKey(t => t.CategoriaId);
            tarea.Property(t => t.Titulo).IsRequired().HasMaxLength(200);
            tarea.Property(t => t.Description).IsRequired(false);
            tarea.Property(t => t.PrioridadTarea);
            tarea.Property(t => t.FechaCreacion);
            //Ignoramos el campo especificado
            tarea.Ignore(t => t.Resumen);

            // Agregamos los datos creados anteriormente con HasData
            tarea.HasData(tareaInit);
        });
    }
}