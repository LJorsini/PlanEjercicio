using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PlanEjercicio.Data;
using PlanEjercicio.Models;
using SQLitePCL;


namespace PlanEjercicio.Controllers;

public class TipoEjerciciosController : Controller
{
    private ApplicationDbContext _context;
    //constructor
    public TipoEjerciciosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {

        return View();
    }

    public JsonResult ListaEjercicio(int? id)
    {
        var tipoDeEjercicios = _context.TipoEjercicios.ToList();

        if (id != null)
        {
            tipoDeEjercicios = tipoDeEjercicios.Where(t => t.IdEjercicio == id).ToList();
        }


        return Json(tipoDeEjercicios);

    }

    public JsonResult GuardarTipoEjercicio(int tipoEjercicioId, string nombreEjercicio)
    {
        string resultado = "";

        if (!String.IsNullOrEmpty(nombreEjercicio))
        {
            nombreEjercicio = nombreEjercicio.ToUpper();

            if (tipoEjercicioId == 0) 
            {
                var existeTipoEjercicio = _context.TipoEjercicios.Where(t => t.NombreEjercicio == nombreEjercicio).Count();
                if (existeTipoEjercicio == 0)
                {
                    var tipoEjercicio = new TipoEjercicio
                    {
                        NombreEjercicio = nombreEjercicio
                    };
                    _context.Add(tipoEjercicio);
                    _context.SaveChanges();
                }
                else
                {
                    resultado = "Ya existe un registro con la misma descripcion";
                }
            }
            else
            {
                //Sino se va a editar el registro
                var tipoEjercicioEditar = _context.TipoEjercicios.Where(t => t.IdEjercicio == tipoEjercicioId ).SingleOrDefault();
                if (tipoEjercicioEditar != null)
                {
                    var existeTipoEjercicio = _context.TipoEjercicios.Where(t => t.NombreEjercicio == nombreEjercicio && t.IdEjercicio != tipoEjercicioId).Count();
                    if (existeTipoEjercicio == 0)
                    {
                        tipoEjercicioEditar.NombreEjercicio = nombreEjercicio;
                        _context.SaveChanges();
                    }
                    else
                    {
                        resultado = "Ya existe un registro con  la misma descripcion";
                    }
                }
            }
        }
        else 
        {
            resultado = "Debe ingresar una descripcion1";
        }

        return Json(resultado);
    }

    public JsonResult EliminarTipoEjercicio (int tipoEjercicioId)
{
    var tipoEjercicio = _context.TipoEjercicios.Find(tipoEjercicioId);
    _context.Remove(tipoEjercicio);
    _context.SaveChanges();
    return Json(true);
}
}




