using API_EntityFramework.Entidades;
using API_EntityFramework.Filtros;
using API_EntityFramework.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_EntityFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AutoresController> _logger;
        private readonly IServicio _servicio;
        public AutoresController(AppDbContext context, ILogger<AutoresController> logger, IServicio servicio)
        {
            _context = context;
            _logger = logger;
            _servicio = servicio;
        }

        // GET: api/<AutoresController>
        [HttpGet]
        [ServiceFilter(typeof(MiFiltroAccion))]
        [Authorize]
        public async Task<ActionResult<List<Autor>>> GetAutores()
        {
            _logger.LogInformation("Obteniendo los autores de base de datos");
            //_servicio.RealizarTarea();
            return await _context.Autores.Include(x => x.Libros).ToListAsync();
        }

        //[HttpGet("{param?}")] - Se puede marcar un parametro como opcional agregando el signo ?
        //[HttpGet("{param= persona}")] - Se puede dar un valor por default a un parametro agregando = value
        [HttpGet("{id:int}")]
        [ResponseCache(Duration =10)] //Proximas respuestas en 10 segundas responde data que este en cache.
        public async Task<ActionResult<Autor>> GetAutor([FromRoute] int id)
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(x => x.Id == id); // Se guarda en cache

            if (autor is null) return NotFound("No existe un autor con este id");

            return autor;
        }

        //Se pueden obtener datos de la cabecera usando [FromHeader]
        //Tambien de [FromQuery] query string ejemplo: //api/autores?nombre=manuel&apellido=barron
        //Existe from services y from form que receiven imagenes, pdf , docs.
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Autor autor)
        {
            bool existe = await _context.Autores.AnyAsync(x => x.Nombre.Equals(autor.Nombre));

            if (existe) return BadRequest($"Ya existe un autor con el nombre {autor.Nombre}");

            _context.Add(autor);
            await _context.SaveChangesAsync();
            return Ok(autor);
        }


        [HttpPut("{id}:int")]
        public async Task<ActionResult> Put(int id, [FromBody] Autor autor)
        {
            if (id != autor.Id)
            {
                return BadRequest("El id del autor no coincide con el id de la URL");

            }
            var existe = await _context.Autores.AnyAsync(x => x.Id == id);
            if (!existe) return NotFound($"No existe ningun autor con el id {id}");

            _context.Update(autor);

            await _context.SaveChangesAsync();
            return Ok(autor);

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _context.Autores.AnyAsync(x => x.Id == id);

            if (!existe) return NotFound($"No existe ningun autor con el id {id}");

            _context.Remove(new Autor() {Id = id });
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}
