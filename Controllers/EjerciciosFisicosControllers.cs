using Microsoft.AspNetCore.Mvc;
using PlanEjercicio.Models;
using PlanEjercicio.Data;


namespace PlanEjercicio.Controllers;

public class EjerciciosFisicosControllers : Controller
{
    private ApplicationDbContext _context;
    //constructor
    public EjerciciosFisicosControllers(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult EjercicioFisico()
    {

        return View();
    }


    public JsonResult ListaTipoDeEjercicio (int? id) {
        var listaEjercicios = _context.EjerciciosFisicos.Find()

    }



}