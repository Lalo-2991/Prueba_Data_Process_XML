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
    public class EfectoComprobanteController : ControllerBase
    {
        private readonly FacturasContext _context;

        public EfectoComprobanteController(FacturasContext context)
        {
            _context = context;
        }

        // GET: api/EfectoComprobante
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EfectoComprobante>>> GetEfectoComprobante()
        {
          if (_context.EfectoComprobante == null)
          {
              return NotFound();
          }
            return await _context.EfectoComprobante.ToListAsync();
        }

        // GET: api/EfectoComprobante/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EfectoComprobante>> GetEfectoComprobante(int id)
        {
          if (_context.EfectoComprobante == null)
          {
              return NotFound();
          }
            var efectoComprobante = await _context.EfectoComprobante.FindAsync(id);

            if (efectoComprobante == null)
            {
                return NotFound();
            }

            return efectoComprobante;
        }

        // PUT: api/EfectoComprobante/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEfectoComprobante(int id, EfectoComprobante efectoComprobante)
        {
            if (id != efectoComprobante.IdEfectoComprobante)
            {
                return BadRequest();
            }

            _context.Entry(efectoComprobante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EfectoComprobanteExists(id))
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

        // POST: api/EfectoComprobante
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EfectoComprobante>> PostEfectoComprobante(EfectoComprobante efectoComprobante)
        {
          if (_context.EfectoComprobante == null)
          {
              return Problem("Entity set 'FacturasContext.EfectoComprobante'  is null.");
          }
            _context.EfectoComprobante.Add(efectoComprobante);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEfectoComprobante", new { id = efectoComprobante.IdEfectoComprobante }, efectoComprobante);
        }

        // DELETE: api/EfectoComprobante/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEfectoComprobante(int id)
        {
            if (_context.EfectoComprobante == null)
            {
                return NotFound();
            }
            var efectoComprobante = await _context.EfectoComprobante.FindAsync(id);
            if (efectoComprobante == null)
            {
                return NotFound();
            }

            _context.EfectoComprobante.Remove(efectoComprobante);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EfectoComprobanteExists(int id)
        {
            return (_context.EfectoComprobante?.Any(e => e.IdEfectoComprobante == id)).GetValueOrDefault();
        }
    }
}
