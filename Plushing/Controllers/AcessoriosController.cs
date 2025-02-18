using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plushing.Data;
using Plushing.Models;

namespace Plushing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcessoriosController : ControllerBase
    {
        private readonly PlushingContext _context;

        public AcessoriosController(PlushingContext context)
        {
            _context = context;
        }

        // GET: api/Acessorios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Acessorio>>> GetAcessorios()
        {
            return await _context.Acessorios.ToListAsync();
        }

        // GET: api/Acessorios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Acessorio>> GetAcessorio(Guid id)
        {
            var acessorio = await _context.Acessorios.FindAsync(id);

            if (acessorio == null)
            {
                return NotFound();
            }

            return acessorio;
        }

        // GET: api/Acessorios/tipo/{tipoAcessorioId}
        [HttpGet("tipo/{tipoAcessorioId}")]
        public async Task<ActionResult<IEnumerable<Acessorio>>> GetAcessoriosByTipo(Guid tipoAcessorioId)
        {
            var acessorios = await _context.Acessorios
                .Where(a => a.TipoAcessorioId == tipoAcessorioId)
                .ToListAsync();

            if (acessorios == null || !acessorios.Any())
            {
                return NotFound();
            }

            return acessorios;
        }

        // PUT: api/Acessorios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAcessorio(Guid id, Acessorio acessorio)
        {
            if (id != acessorio.AcessorioId)
            {
                return BadRequest();
            }

            _context.Entry(acessorio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcessorioExists(id))
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

        // POST: api/Acessorios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Acessorio>> PostAcessorio(Acessorio acessorio)
        {
            _context.Acessorios.Add(acessorio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAcessorio", new { id = acessorio.AcessorioId }, acessorio);
        }

        // DELETE: api/Acessorios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAcessorio(Guid id)
        {
            var acessorio = await _context.Acessorios.FindAsync(id);
            if (acessorio == null)
            {
                return NotFound();
            }

            _context.Acessorios.Remove(acessorio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AcessorioExists(Guid id)
        {
            return _context.Acessorios.Any(e => e.AcessorioId == id);
        }
    }
}
