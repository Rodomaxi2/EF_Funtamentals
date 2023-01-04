using System.ComponentModel.DataAnnotations;

namespace EFProject.Models;

public class Categoria 
{
    //[Key] // Al usar Fluent API no necesitamos la anotaciones para nuestros datos
    public Guid CategoriaId {get;set;}

    // [Required]
    // [MaxLength(150)]
    public string Nombre {get;set;}

    public string Description {get;set;}

    public virtual ICollection<Tarea> Tareas {get;set;}
}