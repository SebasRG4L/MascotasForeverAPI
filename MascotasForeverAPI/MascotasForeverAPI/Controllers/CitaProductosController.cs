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
    public class CitaProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CitaProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CitaProductos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitaProducto>>> GetCitaProductos()
        {
            return await _context.CitaProductos
                .Include(cp => cp.Cita)
                .Include(cp => cp.Producto)
                .ToListAsync();
        }

        // GET: api/CitaProductos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CitaProducto>> GetCitaProducto(int id)
        {
            var citaProducto = await _context.CitaProductos
                .Include(cp => cp.Cita)
                .Include(cp => cp.Producto)
                .FirstOrDefaultAsync(cp => cp.CitaProductoId == id);

            if (citaProducto == null)
            {
                return NotFound();
            }

            return citaProducto;
        }

        // GET: api/CitaProductos/Cita/5
        [HttpGet("Cita/{citaId}")]
        public async Task<ActionResult<IEnumerable<CitaProducto>>> GetProductosByCita(int citaId)
        {
            return await _context.CitaProductos
                .Include(cp => cp.Producto)
                .Where(cp => cp.CitaId == citaId)
                .ToListAsync();
        }

        // PUT: api/CitaProductos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCitaProducto(int id, CitaProducto citaProducto)
        {
            if (id != citaProducto.CitaProductoId)
            {
                return BadRequest();
            }

            _context.Entry(citaProducto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaProductoExists(id))
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

        // POST: api/CitaProductos
        [HttpPost]
        public async Task<ActionResult<CitaProducto>> PostCitaProducto(CitaProducto citaProducto)
        {
            // Actualizar stock del producto automáticamente
            var producto = await _context.Productos.FindAsync(citaProducto.ProductoId);
            if (producto != null && producto.Stock >= citaProducto.Cantidad)
            {
                producto.Stock -= citaProducto.Cantidad;
                _context.CitaProductos.Add(citaProducto);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCitaProducto", new { id = citaProducto.CitaProductoId }, citaProducto);
            }

            return BadRequest("No hay suficiente stock disponible");
        }

        // DELETE: api/CitaProductos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCitaProducto(int id)
        {
            var citaProducto = await _context.CitaProductos.FindAsync(id);
            if (citaProducto == null)
            {
                return NotFound();
            }

            // Devolver el stock al producto
            var producto = await _context.Productos.FindAsync(citaProducto.ProductoId);
            if (producto != null)
            {
                producto.Stock += citaProducto.Cantidad;
            }

            _context.CitaProductos.Remove(citaProducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CitaProductoExists(int id)
        {
            return _context.CitaProductos.Any(e => e.CitaProductoId == id);
        }
    }
}