using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanEjercicio.Data;
using PlanEjercicio.Models;

namespace PlanEjercicio.Controllers;

public class EjerciciosFisicosController : Controller
{
    private ApplicationDbContext _context;

    public EjerciciosFisicosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
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

