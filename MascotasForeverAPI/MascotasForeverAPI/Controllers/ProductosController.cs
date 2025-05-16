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
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos
                .Include(p => p.Proveedor)
                .ToListAsync();
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(p => p.ProductoId == id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // GET: api/Productos/Proveedor/5
        [HttpGet("Proveedor/{proveedorId}")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductosByProveedor(int proveedorId)
        {
            return await _context.Productos
                .Where(p => p.ProveedorId == proveedorId)
                .ToListAsync();
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.ProductoId)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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

        // POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = producto.ProductoId }, producto);
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Productos/5/ActualizarStock
        [HttpPut("{id}/ActualizarStock")]
        public async Task<IActionResult> ActualizarStock(int id, [FromBody] int nuevoStock)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            producto.Stock = nuevoStock;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.ProductoId == id);
        }
    }
}