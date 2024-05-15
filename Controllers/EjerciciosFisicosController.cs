using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlanEjercicio.Data;
using PlanEjercicio.Models;

namespace PlanEjercicio.Controllers;
[Authorize]

public class EjerciciosFisicosController : Controller
{
    private ApplicationDbContext _context;

    public EjerciciosFisicosController(ApplicationDbContext context)
    {
        _context = context;
    }

    /* public IActionResult Index()
    {
        return View();
    } */

    //DropdownList
     public IActionResult Index()
    {
        // Crear una lista de SelectListItem que incluya el elemento adicional
        var selectListItems = new List<SelectListItem>
        {
            new SelectListItem { Value = "0", Text = "[SELECCIONE...]" }
        };

        // Obtener todas las opciones del enum
        var enumValues = Enum.GetValues(typeof(EstadoEmocional)).Cast<EstadoEmocional>();

        // Convertir las opciones del enum en SelectListItem
        selectListItems.AddRange(enumValues.Select(e => new SelectListItem
        {
            Value = e.GetHashCode().ToString(),
            Text = e.ToString().ToUpper()
        }));

        // Pasar la lista de opciones al modelo de la vista
        ViewBag.EstadoEmocionalInicio = selectListItems.OrderBy(t => t.Text).ToList();
        ViewBag.EstadoEmocionalFin = selectListItems.OrderBy(t => t.Text).ToList();

        var tipoEjercicios = _context.TipoEjercicios.ToList();
        tipoEjercicios.Add(new TipoEjercicio{IdEjercicio = 0, NombreEjercicio = "[SELECCIONE...]"});
        ViewBag.TipoEjercicioID = new SelectList(tipoEjercicios.OrderBy(c => c.NombreEjercicio), "IdEjercicio", "NombreEjercicio");

        return View();
    }

    

     public JsonResult ListadoEjerciciosFisicos(int? id)
    {

        
        List<VistaEjercicios> mostrarEjercicios = new List<VistaEjercicios>();
        
        var ejerciciosFisicos = _context.EjerciciosFisicos.ToList();

        
        if (id != null)
        {
            ejerciciosFisicos = ejerciciosFisicos.Where(e => e.EjercicioFisicoId == id).ToList();
        }

        var tiposEjercicios = _context.TipoEjercicios.ToList();

        foreach (var ejercicioFisicos in ejerciciosFisicos)
        {
            var tipoEjercicio = tiposEjercicios.Where(t => t.IdEjercicio == ejercicioFisicos.IdEjercicio).Single();

            var mostrarEjercicioFisico = new VistaEjercicios
            {
                EjercicioFisicoId = ejercicioFisicos.EjercicioFisicoId,
                IdEjercicio = ejercicioFisicos.IdEjercicio,
                TipoEjercicioNombre = tipoEjercicio.NombreEjercicio,
                InicioString = ejercicioFisicos.Inicio.ToString("dd/MM/yyyy HH:mm"),
                FinString = ejercicioFisicos.Fin.ToString("dd/MM/yyyy HH:mm"),
                EstadoEmocionalInicio = Enum.GetName(typeof(EstadoEmocional), ejercicioFisicos.EstadoEmocionalInicio),
                EstadoEmocionalFin = Enum.GetName(typeof(EstadoEmocional), ejercicioFisicos.EstadoEmocionalFin),
                Observaciones = ejercicioFisicos.Observaciones,

            };
            mostrarEjercicios.Add(mostrarEjercicioFisico);
        }

        return Json(mostrarEjercicios);
    }

    public JsonResult CargarEjercicios(int ejercicioFisicoId, int tipoEjercicioID, DateTime fechaInicio, DateTime fechaFin, EstadoEmocional estadoEmocionalInicio, EstadoEmocional estadoEmocionalFin, string? observaciones ) 
    {
        string resultado = "";
        if (ejercicioFisicoId == 0) 
        {
            var ejercicio = new EjercicioFisico
                {
                    EjercicioFisicoId = ejercicioFisicoId,
                    IdEjercicio = tipoEjercicioID,
                    Inicio = fechaInicio,
                    Fin = fechaFin,
                    EstadoEmocionalInicio = estadoEmocionalInicio,
                    EstadoEmocionalFin = estadoEmocionalFin,
                    Observaciones = observaciones,
                };
                _context.Add(ejercicio);
                _context.SaveChanges();
        }
        else 
        {
            var editarEjercicio = _context.EjerciciosFisicos.Where(t => t.EjercicioFisicoId == ejercicioFisicoId).SingleOrDefault();
            {
                var ejercicioExiste = _context.EjerciciosFisicos.Where(t => tipoEjercicioID == ejercicioFisicoId).Count();
                {
                    editarEjercicio.IdEjercicio = tipoEjercicioID;
                    editarEjercicio.Inicio = fechaInicio;
                    editarEjercicio.Fin = fechaFin;
                    editarEjercicio.EstadoEmocionalInicio = estadoEmocionalFin;
                    editarEjercicio.EstadoEmocionalFin = estadoEmocionalFin;
                    editarEjercicio.Observaciones = observaciones;

                }
            }
        }
            return Json (resultado);
    } 

public JsonResult ListadoEjercicios(int? id)
    {
        
        var ejerciciosFisicos = _context.EjerciciosFisicos.ToList();

        
        if (id != null)
        {
            ejerciciosFisicos = ejerciciosFisicos.Where(t => t.EjercicioFisicoId == id).ToList();
        }

        return Json(ejerciciosFisicos);
    }

    public JsonResult EliminarRegistroEjercicio (int ejercicioFisicoId) 
    {
         var ejercicio = _context.EjerciciosFisicos.Find(ejercicioFisicoId);
        _context.Remove(ejercicio);
        _context.SaveChanges();
        return Json(true);

}


}


