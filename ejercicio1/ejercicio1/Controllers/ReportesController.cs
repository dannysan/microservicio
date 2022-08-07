using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ejercicio1.Models;
using Newtonsoft.Json;

namespace ejercicio1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly movimientosContext _context;
        public ReportesController(movimientosContext context)
        {
            _context = context;
        }

        // GET: api/Movimientos/5
        [HttpGet]
        public async Task<ActionResult> ObtenerMovimiento(string Cliente,DateTime FechaDesde,DateTime FechaHasta)
        {
            List<Reporte> reporte = new List<Reporte>();

            if (_context.Movimientos == null)
            {
                return NotFound();
            }
            //Obtener id cliente
            var cliente = from pe in _context.Personas
                          join cl in _context.Clientes on pe.PersId equals cl.PersId
                          join cu in _context.Cuenta on cl.ClieId equals cu.ClieId
                          join mo in _context.Movimientos on cu.CuenId equals mo.CuenId
                          where pe.PersNombre == Cliente && mo.MoviFecha.Value.Date >= FechaDesde.Date
                                && mo.MoviFecha.Value.Date <= FechaHasta.Date
                          select new { Cliente = pe.PersNombre, NumeroCuenta = cu.CuenNumero, Tipo = cu.CuenTipo, SaldoInicial = cu.CuenSaldoInicial,
                              Estado = cu.CuenEstado,Movimiento = mo.MoviValor, SaldoDisponible = mo.MoviSaldo };
                        
            return Ok(cliente);
        }
    }
}
