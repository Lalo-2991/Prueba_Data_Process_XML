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
    public class FormaPagoController : ControllerBase
    {
        private readonly FacturasContext _context;

        public FormaPagoController(FacturasContext context)
        {
            _context = context;
        }

        // GET: api/FormaPago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormaPago>>> GetFormaPago()
        {
          if (_context.FormaPago == null)
          {
              return NotFound();
          }
            return await _context.FormaPago.ToListAsync();
        }

        // GET: api/FormaPago/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FormaPago>> GetFormaPago(int id)
        {
          if (_context.FormaPago == null)
          {
              return NotFound();
          }
            var formaPago = await _context.FormaPago.FindAsync(id);

            if (formaPago == null)
            {
                return NotFound();
            }

            return formaPago;
        }

        // PUT: api/FormaPago/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormaPago(int id, FormaPago formaPago)
        {
            if (id != formaPago.IdFormaPago)
            {
                return BadRequest();
            }

            _context.Entry(formaPago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormaPagoExists(id))
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

        // POST: api/FormaPago
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FormaPago>> PostFormaPago(FormaPago formaPago)
        {
          if (_context.FormaPago == null)
          {
              return Problem("Entity set 'FacturasContext.FormaPago'  is null.");
          }
            _context.FormaPago.Add(formaPago);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFormaPago", new { id = formaPago.IdFormaPago }, formaPago);
        }

        // DELETE: api/FormaPago/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormaPago(int id)
        {
            if (_context.FormaPago == null)
            {
                return NotFound();
            }
            var formaPago = await _context.FormaPago.FindAsync(id);
            if (formaPago == null)
            {
                return NotFound();
            }

            _context.FormaPago.Remove(formaPago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FormaPagoExists(int id)
        {
            return (_context.FormaPago?.Any(e => e.IdFormaPago == id)).GetValueOrDefault();
        }
    }
}
