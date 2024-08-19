using API_EntityFramework.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_EntityFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {

        private readonly AppDbContext _context;

        public LibrosController(AppDbContext context)
        {
            _context = context;
        }




        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> GetLibro(int id)
        {
            return await _context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpGet]
        public async Task<ActionResult<List<Libro>>> GetLibros()
        {
            return await _context.Libros.ToListAsync();
        }


     
        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var existeAutor = await _context.Autores.AnyAsync(x => x.Id == libro.AutorId);

            if (!existeAutor) return BadRequest($"No existe un autor con el id {libro.AutorId}");

            _context.Add(libro);
            await _context.SaveChangesAsync();
            return Ok(libro);
        }


        //[HttpPut("{id}:int")]
        //public async Task<ActionResult> Put(int id, [FromBody] Autor autor)
        //{
        //    if (id != autor.Id)
        //    {
        //        return BadRequest("El id del libro no coincide con el id de la URL");

        //    }
        //    var existe = await _context.Libros.AnyAsync(x => x.Id == id);
        //    if (!existe) return NotFound($"No existe ningun libro con el id {id}");

        //    _context.Update(autor);

        //    await _context.SaveChangesAsync();
        //    return Ok(autor);

        //}

        //[HttpDelete("{id:int}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    var existe = await _context.Libros.AnyAsync(x => x.Id == id);
        //    if (!existe) return NotFound($"No existe ningun libro con el id {id}");

        //    _context.Remove(new Libro() { Id = id });
        //    await _context.SaveChangesAsync();

        //    return Ok();

        //}
    }
}
