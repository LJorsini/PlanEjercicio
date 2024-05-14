using System.ComponentModel.DataAnnotations;

namespace PlanEjercicio.Models
{
    public class EjercicioFisico
    {
        [Key]
        public int EjercicioFisicoId { get; set; }
        public int IdEjercicio { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public EstadoEmocional EstadoEmocionalInicio { get; set; }
        public EstadoEmocional EstadoEmocionalFin { get; set; }
        public string? Observaciones { get; set; }
        /* public virtual TipoEjercicio TipoEjercicio { get; set; } */
    }

        public class VistaEjercicios
        {
            public int EjercicioFisicoId {get; set;}
            public int IdEjercicio {get; set;}
            public string? TipoEjercicioNombre {get; set;}
            public string? InicioString {get; set;}
            public string? FinString {get; set;}
            public string? EstadoEmocionalInicio { get; internal set; }
            public string? EstadoEmocionalFin { get; internal set; }
            public string? Observaciones {get; set;}


        }

    public enum EstadoEmocional
    {
        Feliz = 1,
        Triste,
        Enojado,
        Ansioso,
        Estresado,
        Relajado,
        Aburrido,
        Emocionado,
        Agobiado,
        Confundido,
        Optimista,
        Pesimista,
        Motivado,
        Cansado,
        Eufórico,
        Agitado,
        Satisfecho,
        Desanimado
    }
}



