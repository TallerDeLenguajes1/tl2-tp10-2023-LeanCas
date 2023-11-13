using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class TableroController : ControllerBase
{

    private readonly ILogger<TableroController> _logger;

    private readonly ITableroRepositorio repository;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        repository = new TableroRepositorio();
    }

    [HttpPost]
    [Route("CrearTablero")]
    public ActionResult<Tarea> CrearTablero(int idTablero)
    {
        return Ok("El tablero fue creado con exito");
    }

    [HttpGet]
    [Route("ListarTableros")]
    public ActionResult<List<Tablero>> ListarTableros()
    {
        var tableros = repository.GetAll();
        if (tableros == null)
        {
            return BadRequest("No se pudo listar ningun tablero");
        }
        else
        {
            return Ok("Tableros listados: " + tableros);
        }
    }

}