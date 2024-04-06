using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PlanEjercicio.Models;

namespace PlanEjercicio.Controllers;

public class TipoEjercicioController : Controller 
{
    private readonly ILogger<TipoEjercicioController> _logger;

    public TipoEjercicioController(ILogger<TipoEjercicioController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}