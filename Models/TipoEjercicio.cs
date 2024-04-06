using System.ComponentModel.DataAnnotations;

public class TipoEjercicio {
    [Key]
    public int IdEjercicio;
    
    [Required(ErrorMessage = "Se requiere nombre de ejercicio")]
    public string? NombreEjercicio;
}