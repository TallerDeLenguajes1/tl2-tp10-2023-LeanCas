using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tp10_tl2.Models;

namespace tp10_tl2.Controllers;

public class TareaController : Controller
{

    private readonly ILogger<TareaController> _logger;

    private readonly ITareaRepositorio repository;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        repository = new TareaRepositorio();
    }
    /* CREAR TAREA
        [HttpPost]
        [Route("CrearTarea")]
        public ActionResult<Usuario> CrearTarea(Tarea tarea)
        {
            repository.Create()
            return Ok("El usuario fue creado con exito" + usuario);
        }
    */

    public IActionResult Index()
    {
        var listaTareas = repository.GetAll();
        return View(listaTareas);
    }


    [HttpGet]
    public IActionResult TareasUsuario(int id)
    {
        var listaTareas = repository.GetAll();
        var listaTareasPorId = listaTareas.FindAll(T => T.IdUsuarioAsignado == id);
        return View(listaTareasPorId);
    }

    [HttpGet]
    public IActionResult ModificarTarea(int id)
    {
        var tarea = repository.GetTarea(id);
        return View(tarea);
    }

    [HttpPost]
    public IActionResult ModificarTarea(int id, Tarea tarea)
    {
        repository.Set(id, tarea);
        return RedirectToAction("Index");
    }
    public IActionResult EliminarTarea(int id)
    {
        repository.Delete(id);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}