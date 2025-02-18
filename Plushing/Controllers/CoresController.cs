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
    public class CoresController : ControllerBase
    {
        private readonly PlushingContext _context;

        public CoresController(PlushingContext context)
        {
            _context = context;
        }

        // GET: api/Cores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cor>>> GetCores()
        {
            return await _context.Cores.ToListAsync();
        }

        // GET: api/Cores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cor>> GetCor(Guid id)
        {
            var cor = await _context.Cores.FindAsync(id);

            if (cor == null)
            {
                return NotFound();
            }

            return cor;
        }

        // PUT: api/Cores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCor(Guid id, Cor cor)
        {
            if (id != cor.CorId)
            {
                return BadRequest();
            }

            _context.Entry(cor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CorExists(id))
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

        // POST: api/Cores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cor>> PostCor(Cor cor)
        {
            _context.Cores.Add(cor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCor", new { id = cor.CorId }, cor);
        }

        // DELETE: api/Cores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCor(Guid id)
        {
            var cor = await _context.Cores.FindAsync(id);
            if (cor == null)
            {
                return NotFound();
            }

            _context.Cores.Remove(cor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CorExists(Guid id)
        {
            return _context.Cores.Any(e => e.CorId == id);
        }

        // GET: api/Cores/nome/{nome}
        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<Cor>> GetCorByName(string nome)
        {
            var cor = await _context.Cores.FirstOrDefaultAsync(c => c.Nome == nome);

            if (cor == null)
            {
                return NotFound();
            }

            return cor;
        }

        // GET: api/Cores/codigoHex/{codigoHex}
        [HttpGet("codigoHex/{codigoHex}")]
        public async Task<ActionResult<Cor>> GetCorByCodigoHex(string codigoHex)
        {
            var cor = await _context.Cores.FirstOrDefaultAsync(c => c.CodigoHex == codigoHex);

            if (cor == null)
            {
                return NotFound();
            }

            return cor;
        }

    }
}
