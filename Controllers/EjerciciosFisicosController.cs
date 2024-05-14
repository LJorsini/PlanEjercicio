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
            ejerciciosFisicos = ejerciciosFisicos.Where(t => t.EjercicioFisicoId == id).ToList();
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
                Observaciones = ejercicioFisicos.Observaciones,

            };
            mostrarEjercicios.Add(mostrarEjercicioFisico);
        }

        return Json(mostrarEjercicios);
    }



}

