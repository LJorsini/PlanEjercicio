using System.ComponentModel.DataAnnotations;

namespace PlanEjercicio.Models
{
    public class TipoEjercicio
    {
        [Key]
        public int IdEjercicio { get; set; }

        [Required(ErrorMessage = "Se requiere nombre de ejercicio")]
        public string? NombreEjercicio { get; set; }
        public bool Eliminado { get; set; }
        public virtual ICollection<EjercicioFisico> EjerciciosFisicos { get; set; }
    }
}



