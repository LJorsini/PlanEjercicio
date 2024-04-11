using System.ComponentModel.DataAnnotations;

namespace PlanEjercicio.Models;

public class TipoEjercicio {
    [Key]
    public int IdEjercicio {get; set;}
    
    [Required(ErrorMessage = "Se requiere nombre de ejercicio")]
    public string? NombreEjercicio {get; set;}
}