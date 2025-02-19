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
    public class PadraosController : ControllerBase
    {
        private readonly PlushingContext _context;

        public PadraosController(PlushingContext context)
        {
            _context = context;
        }

        // GET: api/Padraos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Padrao>>> GetPadroes()
        {
            return await _context.Padroes.ToListAsync();
        }

        // GET: api/Padraos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Padrao>> GetPadrao(Guid id)
        {
            var padrao = await _context.Padroes.FindAsync(id);

            if (padrao == null)
            {
                return NotFound();
            }

            return padrao;
        }

        // GET: api/Padraos/descricao/{descricao}
        [HttpGet("descricao/{descricao}")]
        public async Task<ActionResult<IEnumerable<Padrao>>> GetPadroesByDescricao(string descricao)
        {
            var padroes = await _context.Padroes
                .Where(p => p.Descricao.Equals(descricao, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            if (padroes == null || !padroes.Any())
            {
                return NotFound();
            }

            return padroes;
        }

        // PUT: api/Padraos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPadrao(Guid id, Padrao padrao)
        {
            if (id != padrao.PadraoId)
            {
                return BadRequest();
            }

            _context.Entry(padrao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PadraoExists(id))
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

        // POST: api/Padraos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Padrao>> PostPadrao(Padrao padrao)
        {
            _context.Padroes.Add(padrao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPadrao", new { id = padrao.PadraoId }, padrao);
        }

        // DELETE: api/Padraos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePadrao(Guid id)
        {
            var padrao = await _context.Padroes.FindAsync(id);
            if (padrao == null)
            {
                return NotFound();
            }

            _context.Padroes.Remove(padrao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PadraoExists(Guid id)
        {
            return _context.Padroes.Any(e => e.PadraoId == id);
        }
    }
}
