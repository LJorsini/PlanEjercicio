using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PlanEjercicio.Data;
using PlanEjercicio.Models;


namespace PlanEjercicio.Controllers;

public class TipoEjercicioController : Controller 
{
    private ApplicationDbContext _context;
    //constructor
    public TipoEjercicioController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index1()
    {

        return View();
    }

    public JsonResult ListadoTipoEjercicio(int? id) 
    {
        var tipoDeEjercicios = _context.TipoEjercicios.ToList();

        if (id != null) {
            tipoDeEjercicios  = tipoDeEjercicios.Where(t => t.IdEjercicio == id).ToList();
        }


        return Json(tipoDeEjercicios);

    }


}