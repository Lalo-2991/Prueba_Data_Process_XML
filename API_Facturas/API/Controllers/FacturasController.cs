using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models.Context;
using API.Models.Entidades;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly FacturasContext _context;

        public FacturasController(FacturasContext context)
        {
            _context = context;
        }

        // GET: api/Facturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Encabezado>>> GetEncabezado()
        {
          if (_context.Encabezado == null)
          {
              return NotFound();
          }
            return await _context.Encabezado.ToListAsync();
        }

        // GET: api/Facturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Encabezado>> GetEncabezado(int id)
        {
          if (_context.Encabezado == null)
          {
              return NotFound();
          }
            var encabezado = await _context.Encabezado.FindAsync(id);

            if (encabezado == null)
            {
                return NotFound();
            }

            return encabezado;
        }

        // PUT: api/Facturas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEncabezado(int id, Encabezado oEncabezado)
        {

            if (id != oEncabezado.Id)
            {
                return BadRequest();
            }

            _context.Entry(oEncabezado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncabezadoExists(id))
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

        // POST: api/Facturas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Encabezado>> PostEncabezado(Encabezado encabezado)
        {
          if (_context.Encabezado == null)
          {
              return Problem("Entity set 'FacturasContext.Encabezado'  is null.");
          }
            _context.Encabezado.Add(encabezado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEncabezado", new { id = encabezado.Id }, encabezado);
        }

        // DELETE: api/Facturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEncabezado(int id)
        {
            if (_context.Encabezado == null)
            {
                return NotFound();
            }
            var encabezado = await _context.Encabezado.FindAsync(id);
            if (encabezado == null)
            {
                return NotFound();
            }

            _context.Encabezado.Remove(encabezado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EncabezadoExists(int id)
        {
            return (_context.Encabezado?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
