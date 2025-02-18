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
    public class TamanhosController : ControllerBase
    {
        private readonly PlushingContext _context;

        public TamanhosController(PlushingContext context)
        {
            _context = context;
        }

        // GET: api/Tamanhos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tamanho>>> GetTamanhos()
        {
            return await _context.Tamanhos.ToListAsync();
        }

        // GET: api/Tamanhos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tamanho>> GetTamanho(Guid id)
        {
            var tamanho = await _context.Tamanhos.FindAsync(id);

            if (tamanho == null)
            {
                return NotFound();
            }

            return tamanho;
        }

        // GET: api/Tamanhos/descricao/{descricao}
        [HttpGet("descricao/{descricao}")]
        public async Task<ActionResult<IEnumerable<Tamanho>>> GetTamanhosByDescricao(string descricao)
        {
            var tamanhos = await _context.Tamanhos
                .Where(t => t.Descricao.Equals(descricao, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            if (tamanhos == null || !tamanhos.Any())
            {
                return NotFound();
            }

            return tamanhos;
        }

        // PUT: api/Tamanhos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTamanho(Guid id, Tamanho tamanho)
        {
            if (id != tamanho.TamanhoId)
            {
                return BadRequest();
            }

            _context.Entry(tamanho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TamanhoExists(id))
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

        // POST: api/Tamanhos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tamanho>> PostTamanho(Tamanho tamanho)
        {
            _context.Tamanhos.Add(tamanho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTamanho", new { id = tamanho.TamanhoId }, tamanho);
        }

        // DELETE: api/Tamanhos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTamanho(Guid id)
        {
            var tamanho = await _context.Tamanhos.FindAsync(id);
            if (tamanho == null)
            {
                return NotFound();
            }

            _context.Tamanhos.Remove(tamanho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TamanhoExists(Guid id)
        {
            return _context.Tamanhos.Any(e => e.TamanhoId == id);
        }
    }
}
