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
    public class ClientesController : ControllerBase
    {
        private readonly movimientosContext _context;

        public ClientesController(movimientosContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<List<PersonaCliente>>> GetClientes()
        {
            if (_context.Clientes == null || _context.Personas == null)
            {
                return NotFound();
            }
            List<PersonaCliente> personaCliente = new List<PersonaCliente>();
            var personas = await _context.Personas.ToListAsync();
            var clientes = await _context.Clientes.ToListAsync();
            foreach (Persona pers in personas)
            {
                personaCliente.Add(new PersonaCliente()
                {
                    PersNombre = pers.PersNombre,
                    PersDireccion = pers.PersDireccion,
                    PersEdad = pers.PersEdad,
                    PersTelefono = pers.PersTelefono,
                    PersGenero = pers.PersGenero,
                    PersIdentificacion = pers.PersIdentificacion,
                    ClieContrasenia = clientes.FirstOrDefault(x => x.PersId == pers.PersId).ClieContrasenia,
                    ClieEstado = clientes.FirstOrDefault(x => x.PersId == pers.PersId).ClieEstado,
                    PersId = pers.PersId,
                    ClieId = clientes.FirstOrDefault(x => x.PersId == pers.PersId).ClieId
                });
            }

            return personaCliente;
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, PersonaCliente cliente)
        {
            if (id != cliente.ClieId)
            {
                return BadRequest();
            }
            Persona persona = new Persona()
            {
                PersNombre = cliente.PersNombre,
                PersDireccion = cliente.PersDireccion,
                PersEdad = cliente.PersEdad,
                PersTelefono = cliente.PersTelefono,
                PersGenero = cliente.PersGenero,
                PersIdentificacion = cliente.PersIdentificacion,
                PersId = (int)cliente.PersId
            };

            _context.Entry(persona).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }


            Cliente cliente1 = new Cliente()
            {
                ClieContrasenia = cliente.ClieContrasenia,
                ClieEstado = cliente.ClieEstado,
                ClieId = cliente.ClieId,
                PersId = cliente.PersId
            };
            _context.Entry(cliente1).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(PersonaCliente cliente)
        {
            //Primero ingresa persona
            if (_context.Personas == null)
            {
                return Problem("Entity set 'movimientosContext.Personas'  is null.");
            }
            Persona persona = new Persona()
            {
                PersNombre = cliente.PersNombre,
                PersDireccion = cliente.PersDireccion,
                PersEdad = cliente.PersEdad,
                PersTelefono = cliente.PersTelefono,
                PersGenero = cliente.PersGenero,
                PersIdentificacion = cliente.PersIdentificacion,
            };
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            IEnumerable<Persona> personas = await _context.Personas.ToListAsync();
            int intPersId = personas.Last().PersId;

            if (_context.Clientes == null)
            {
                return Problem("Entity set 'movimientosContext.Clientes'  is null.");
            }
            Cliente cliente1 = new Cliente()
            {
                ClieContrasenia = cliente.ClieContrasenia,
                ClieEstado = cliente.ClieEstado,
                PersId = intPersId
            };

            _context.Clientes.Add(cliente1);
            await _context.SaveChangesAsync();
            //Obtiene el id del cliente
            IEnumerable<Cliente> clientes = await _context.Clientes.ToListAsync();
            int intClieId = clientes.Last().ClieId;
            return CreatedAtAction("GetCliente", new { id = cliente.ClieId }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            // Elimina persona
            var persona = await _context.Personas.FindAsync(cliente.PersId);
            if (persona == null)
            {
                return NotFound();
            }
            _context.Personas.Remove(persona);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return (_context.Clientes?.Any(e => e.ClieId == id)).GetValueOrDefault();
        }
    }
}
