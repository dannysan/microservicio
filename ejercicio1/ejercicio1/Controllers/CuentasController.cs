using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ejercicio1.Models;

namespace ejercicio1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly movimientosContext _context;

        public CuentasController(movimientosContext context)
        {
            _context = context;
        }

        // GET: api/Cuentas
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Cuenta>>> ObtenerCuentas()
        {
          if (_context.Cuenta == null)
          {
              return NotFound();
          }
            return await _context.Cuenta.ToListAsync();
        }

        // GET: api/Cuentas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cuenta>> ObtenerCuenta(int id)
        {
          if (_context.Cuenta == null)
          {
              return NotFound();
          }
            var cuentum = await _context.Cuenta.FindAsync(id);

            if (cuentum == null)
            {
                return NotFound();
            }

            return cuentum;
        }

        // PUT: api/Cuentas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarCuenta(int id, Cuenta cuentum)
        {
            if (id != cuentum.CuenId)
            {
                return BadRequest();
            }

            _context.Entry(cuentum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaExists(id))
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

        // POST: api/Cuentas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cuenta>> AgregarCuenta(Cuenta cuentum)
        {
          if (_context.Cuenta == null)
          {
              return Problem("Entity set 'movimientosContext.Cuenta'  is null.");
          }
            _context.Cuenta.Add(cuentum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("AgregarCuenta", new { id = cuentum.CuenId }, cuentum);
        }

        // DELETE: api/Cuentas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCuenta(int id)
        {
            if (_context.Cuenta == null)
            {
                return NotFound();
            }
            var cuentum = await _context.Cuenta.FindAsync(id);
            if (cuentum == null)
            {
                return NotFound();
            }

            _context.Cuenta.Remove(cuentum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CuentaExists(int id)
        {
            return (_context.Cuenta?.Any(e => e.CuenId == id)).GetValueOrDefault();
        }
    }
}
