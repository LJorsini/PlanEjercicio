using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanEjercicio.Data;
using PlanEjercicio.Models;
using SQLitePCL;


namespace PlanEjercicio.Controllers;
[Authorize]
public class TipoEjerciciosController : Controller
{
    private  ApplicationDbContext _context;
    //constructor
    public TipoEjerciciosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {

        return View();
    }

    //Metodo para mostrar la lista de los tipos de ejercicios
    public JsonResult ListadoEjercicios(int? id)
    {
        //se guarda en una variable el listado completo de los tipos de ejercicio
        var tipoEjercicios = _context.TipoEjercicios.ToList();

        //si el usuario ingresa un id pasa lo siguiente
        if (id != null)
        {
            tipoEjercicios = tipoEjercicios.Where(t => t.IdEjercicio == id).ToList();
        }

        return Json(tipoEjercicios);
    }

    //Metodo para nuevo ejercicio
    public JsonResult CargarNuevoEjercicio(int tipoEjercicioId, string descripcion)
    {
        string resultado = "";

        if (!String.IsNullOrEmpty(descripcion))
        {
            descripcion = descripcion.ToUpper();

            //Este if verifica si se esta creando o editando un nuevo registro

            if (tipoEjercicioId == 0)
            {
                //verifico si existe un registro en la base de datos
                var existeEjercicio = _context.TipoEjercicios.Where(t => t.NombreEjercicio == descripcion).Count();
                if (existeEjercicio == 0)
                {
                    //Guardo el ejercicio
                    var tipoEjercicio = new TipoEjercicio
                    {
                        NombreEjercicio = descripcion
                    };
                    _context.Add(tipoEjercicio);
                    _context.SaveChanges();
                }
                else
                {
                    resultado = "Ya esxite un registro con la misma descripcion";
                }
            }
            else
            {
                //Se va a editar el registro
                var editarTipoEjercicio = _context.TipoEjercicios.Where(t => t.IdEjercicio == tipoEjercicioId).SingleOrDefault();
                if (editarTipoEjercicio != null)
                {
                    //busco si existe un registro con el mismo nombre pero distinto id
                    var existeEjercicio = _context.TipoEjercicios.Where(t => t.NombreEjercicio == descripcion && t.IdEjercicio == tipoEjercicioId).Count();
                    if (existeEjercicio == 0)
                    {
                        editarTipoEjercicio.NombreEjercicio = descripcion;
                        _context.SaveChanges();
                    }
                    else
                    {
                        resultado = "YA existe un registro con la misma descripcion";
                    }
                }
            }
        }
        else
        {
            resultado = "Debe ingresar una descripcion";
        }
        return Json(resultado);
    }


    //Metodo para eliminar el ejercicio
    public JsonResult EliminarRegistro(int idEjercicio)
    {
        var tipoEjercicio = _context.TipoEjercicios.Find(idEjercicio);
        _context.Remove(tipoEjercicio);
        _context.SaveChanges();
        return Json(true);
    }

}

