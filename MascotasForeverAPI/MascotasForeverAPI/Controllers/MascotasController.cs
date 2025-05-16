using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MascotasForeverAPI.Data;
using MascotasForeverAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MascotasForeverAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MascotasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Mascotas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mascota>>> GetMascotas()
        {
            return await _context.Mascotas.Include(m => m.Cliente).ToListAsync();
        }

        // GET: api/Mascotas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mascota>> GetMascota(int id)
        {
            var mascota = await _context.Mascotas
                .Include(m => m.Cliente)
                .FirstOrDefaultAsync(m => m.MascotaId == id);

            if (mascota == null)
            {
                return NotFound();
            }

            return mascota;
        }

        // GET: api/Mascotas/Cliente/5
        [HttpGet("Cliente/{clienteId}")]
        public async Task<ActionResult<IEnumerable<Mascota>>> GetMascotasByCliente(int clienteId)
        {
            return await _context.Mascotas
                .Where(m => m.ClienteId == clienteId)
                .ToListAsync();
        }

        // PUT: api/Mascotas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMascota(int id, Mascota mascota)
        {
            if (id != mascota.MascotaId)
            {
                return BadRequest();
            }

            _context.Entry(mascota).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MascotaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Mascotas
        [HttpPost]
        public async Task<ActionResult<Mascota>> PostMascota(Mascota mascota)
        {
            _context.Mascotas.Add(mascota);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMascota", new { id = mascota.MascotaId }, mascota);
        }

        // DELETE: api/Mascotas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMascota(int id)
        {
            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null)
            {
                return NotFound();
            }

            _context.Mascotas.Remove(mascota);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MascotaExists(int id)
        {
            return _context.Mascotas.Any(e => e.MascotaId == id);
        }
    }
}