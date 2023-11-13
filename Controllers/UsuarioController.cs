using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : Controller
{

    private readonly ILogger<UsuarioController> _logger;

    private readonly IUsuarioRepositorio repository;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        repository = new UsuarioRepositorio();
    }
    public IActionResult Index()
    {
        var usuarios = repository.GetAll();
        return View(usuarios);
    }

    [HttpGet("ModificarUsuario/{id}")]
    public IActionResult ModificarUsuario(int id)
    {
        var usuario = repository.GetUsuario(id);
        return View(usuario);
    }


    [HttpGet("CrearUsuario")]
    public IActionResult CrearUsuario()
    {
        return View(new Usuario());
    }

    [HttpPost]
    public IActionResult CrearUsuarioForm([FromForm] Usuario usuario) // Le añadi el [FromForm] porque sino no anda 
    {
        repository.Create(usuario);
        return RedirectToAction("Index");
    }

    [HttpPut]
    public IActionResult ModificarUsuarioForm(int id, [FromForm] Usuario usuario)
    {
        repository.Set(id, usuario);
        return RedirectToAction("Index");
    }


    [HttpDelete("EliminarUsuario/{id}")]
    public IActionResult EliminarUsuario(int id)
    {
        repository.Delete(id);
        return RedirectToAction("Index");
    }

}