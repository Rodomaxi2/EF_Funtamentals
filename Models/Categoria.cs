using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EFProject.Models;

public class Categoria 
{
    //[Key] // Al usar Fluent API no necesitamos la anotaciones para nuestros datos
    public Guid CategoriaId {get;set;}

    // [Required]
    // [MaxLength(150)]
    public string Nombre {get;set;}

    public string Description {get;set;}

    public int Peso {get;set;}

    [JsonIgnore]
    public virtual ICollection<Tarea> Tareas {get;set;}
}