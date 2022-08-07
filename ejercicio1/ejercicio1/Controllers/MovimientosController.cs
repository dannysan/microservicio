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
    public class MovimientosController : ControllerBase
    {
        private readonly movimientosContext _context;

        public MovimientosController(movimientosContext context)
        {
            _context = context;
        }

        // GET: api/Movimientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movimiento>>> ObtenerMovimientos()
        {
            if (_context.Movimientos == null)
            {
                return NotFound();
            }
            return await _context.Movimientos.ToListAsync();
        }

        // GET: api/Movimientos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movimiento>> ObtenerMovimiento(int id)
        {
            if (_context.Movimientos == null)
            {
                return NotFound();
            }
            var movimiento = await _context.Movimientos.FindAsync(id);

            if (movimiento == null)
            {
                return NotFound();
            }

            return movimiento;
        }

        // PUT: api/Movimientos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarMovimiento(int id, Movimiento movimiento)
        {
            if (id != movimiento.MoviId)
            {
                return BadRequest();
            }

            _context.Entry(movimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovimientoExists(id))
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

        // POST: api/Movimientos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movimiento>> AgregarMovimiento(MovimientoCuenta movimientoCuenta)
        {
            Movimiento movimiento = new Movimiento();
            double? dblSaldoDisponible = 0;
            double dblMontoDiario = 1000;

            if (_context.Movimientos == null)
            {
                return Problem("Entity set 'movimientosContext.Movimientos'  is null.");
            }
            //Obtener IdCuenta
            Cuenta? cuenta = await _context.Cuenta.FirstOrDefaultAsync(x => x.CuenNumero == movimientoCuenta.NumeroCuenta);
            //Obtiene movimientos por cliente
            var movimientos = await _context.Movimientos.Where(x => x.CuenId == cuenta.CuenId).ToListAsync();

            //Validación monto límite superado
            if (movimientos != null && movimientos.Count > 0)
            {
                var cuentaMovimientos = movimientos.Where(x => x.MoviFecha.Value.Date == movimientoCuenta.Fecha.Value.Date);
                var TotalMovimientos = cuentaMovimientos.Where(x => x.MoviValor < 0).Sum(x => x.MoviValor);
                if (TotalMovimientos > dblMontoDiario)
                {
                    return Problem("Cupo diario excedido");
                }
            }
            //Validación para ingreso de valores
            if ( movimientos.Count == 0)
            {
                if (movimientoCuenta.Valor < 0)
                {
                    if (cuenta.CuenSaldoInicial < (movimientoCuenta.Valor * -1))
                    {
                        return Problem("Saldo no disponible");
                    }
                    else
                    {
                        dblSaldoDisponible = cuenta.CuenSaldoInicial + movimientoCuenta.Valor;
                    }
                }
                else
                {
                    dblSaldoDisponible = cuenta.CuenSaldoInicial + movimientoCuenta.Valor;
                }
                movimiento.CuenId = cuenta.CuenId;
                movimiento.MoviFecha = movimientoCuenta.Fecha;
                movimiento.MoviValor = movimientoCuenta.Valor;
                movimiento.MoviSaldo = dblSaldoDisponible;

            }
            else // cuando ya existen movimientos
            {
                var ultimoMovimiento = movimientos.OrderByDescending(x => x.MoviFecha).FirstOrDefault();
                if (movimientoCuenta.Valor < 0)
                {
                    if (ultimoMovimiento.MoviSaldo < (movimientoCuenta.Valor * -1))
                    {
                        return Problem("Saldo no disponible");
                    }
                    else
                    {
                        dblSaldoDisponible = ultimoMovimiento.MoviSaldo + movimientoCuenta.Valor;
                    }
                }
                else
                {
                    dblSaldoDisponible = ultimoMovimiento.MoviSaldo + movimientoCuenta.Valor;
                }
                movimiento.CuenId = cuenta.CuenId;
                movimiento.MoviFecha = movimientoCuenta.Fecha;
                movimiento.MoviValor = movimientoCuenta.Valor;
                movimiento.MoviSaldo = dblSaldoDisponible;
            }

            _context.Movimientos.Add(movimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("AgregarMovimiento", new { id = movimiento.MoviId }, movimiento);
        }

        // DELETE: api/Movimientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarMovimiento(int id)
        {
            if (_context.Movimientos == null)
            {
                return NotFound();
            }
            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }

            _context.Movimientos.Remove(movimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovimientoExists(int id)
        {
            return (_context.Movimientos?.Any(e => e.MoviId == id)).GetValueOrDefault();
        }
    }
}
