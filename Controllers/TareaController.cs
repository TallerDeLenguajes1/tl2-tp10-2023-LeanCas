using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tp10_tl2.Models;
using TareaRepository;
using TableroRepository;
using UsuarioRepository;

namespace tp10_tl2.Controllers;

public class TareaController : Controller
{

    private readonly ILogger<TareaController> _logger;

    private readonly ITareaRepositorio repository;
    private readonly ITableroRepositorio TableroRepository;
    private readonly IUsuarioRepositorio UsuarioRepository;
    private readonly ListarTareaViewModel listartareaVM = new ListarTareaViewModel();

    public TareaController(ILogger<TareaController> logger, ITareaRepositorio TareaRepository, ITableroRepositorio tableroRepositorio, IUsuarioRepositorio usuarioRepositorio)
    {
        _logger = logger;
        repository = TareaRepository;
        TableroRepository = tableroRepositorio;
        UsuarioRepository = usuarioRepositorio;
    }

    public IActionResult Index()
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")) && (HttpContext.Session.GetString("rol")) == "Administrador")
        {
            var listaTareas = repository.GetAll();
            var tareaslistaVM = new List<ListarTareaViewModel>();
            foreach (Tarea T in listaTareas)
            {
                tareaslistaVM.Add(listartareaVM.convertirVM(T, UsuarioRepository.GetUsuario(T.IdUsuarioAsignado).NombreDeUsuario, TableroRepository.GetTablero(T.IdTablero).Nombre));
            }

            return View(tareaslistaVM);

        }
        else
        {
            return RedirectToRoute(new { controller = "Logueo", action = "Index" });
        }

    }

    [HttpGet]
    public IActionResult CrearTarea(int id)
    {
        return View(new CrearTareaViewModel(GetUsuarios(), GetTableros()));
    }

    [HttpPost]
    public IActionResult CrearTarea(CrearTareaViewModel tareaViewModel)
    {
        if (!ModelState.IsValid) return RedirectToAction("Index");
        var tarea = new Tarea(tareaViewModel.Nombre, tareaViewModel.Descripcion, tareaViewModel.Color, tareaViewModel.Estado, tareaViewModel.IdUsuarioAsignado, tareaViewModel.IdTablero);
        repository.Create(tarea.IdTablero, tarea);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult TareasUsuario(int id)
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")) && (HttpContext.Session.GetString("rol")) == "Administrador")
        {
            var listaTareasAll = repository.GetAll();
            var listaTareas = listaTareasAll.FindAll(T => T.IdUsuarioAsignado == id);
            var tareaslistaVM = new List<ListarTareaViewModel>();
            foreach (Tarea T in listaTareas)
            {
                tareaslistaVM.Add(listartareaVM.convertirVM(T, UsuarioRepository.GetUsuario(T.IdUsuarioAsignado).NombreDeUsuario, TableroRepository.GetTablero(T.IdTablero).Nombre));
            }
            return View(tareaslistaVM);

        }
        else
        {
            return RedirectToRoute(new { controller = "Logueo", action = "Index" });
        }
    }

    [HttpGet]
    public IActionResult ModificarTarea(int id)
    {
        var tarea = repository.GetTarea(id);
        var tareaViewModel = new ModificarTareaViewModel(tarea);
        return View(tareaViewModel);
    }

    [HttpPost]
    public IActionResult ModificarTarea(int id, ModificarTareaViewModel tareaViewModel)
    {
        if (!ModelState.IsValid) return RedirectToAction("Index");
        var viewModel = new ModificarTareaViewModel();
        var tarea = viewModel.convertirTarea(tareaViewModel);
        repository.Set(id, tarea);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarTarea(int id)
    {
        repository.Delete(id);
        return RedirectToAction("Index");
    }

    private List<Usuario> GetUsuarios()
    {
        var listaUsuario = UsuarioRepository.GetAll();
        return listaUsuario;

    }

    private List<Tablero> GetTableros()
    {
        var listaTablero = TableroRepository.GetAll();
        return listaTablero;
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}