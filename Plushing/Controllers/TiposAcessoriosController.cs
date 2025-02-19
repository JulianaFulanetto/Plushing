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
    public class TiposAcessoriosController : ControllerBase
    {
        private readonly PlushingContext _context;

        public TiposAcessoriosController(PlushingContext context)
        {
            _context = context;
        }

        // GET: api/TiposAcessorios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoAcessorio>>> GetTiposAcessorios()
        {
            return await _context.TiposAcessorios.ToListAsync();
        }

        // GET: api/TiposAcessorios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoAcessorio>> GetTipoAcessorio(Guid id)
        {
            var tipoAcessorio = await _context.TiposAcessorios.FindAsync(id);

            if (tipoAcessorio == null)
            {
                return NotFound();
            }

            return tipoAcessorio;
        }

        // PUT: api/TiposAcessorios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoAcessorio(Guid id, TipoAcessorio tipoAcessorio)
        {
            if (id != tipoAcessorio.TipoAcessorioId)
            {
                return BadRequest();
            }

            _context.Entry(tipoAcessorio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoAcessorioExists(id))
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

        // POST: api/TiposAcessorios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoAcessorio>> PostTipoAcessorio(TipoAcessorio tipoAcessorio)
        {
            _context.TiposAcessorios.Add(tipoAcessorio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoAcessorio", new { id = tipoAcessorio.TipoAcessorioId }, tipoAcessorio);
        }

        // DELETE: api/TiposAcessorios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoAcessorio(Guid id)
        {
            var tipoAcessorio = await _context.TiposAcessorios.FindAsync(id);
            if (tipoAcessorio == null)
            {
                return NotFound();
            }

            _context.TiposAcessorios.Remove(tipoAcessorio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoAcessorioExists(Guid id)
        {
            return _context.TiposAcessorios.Any(e => e.TipoAcessorioId == id);
        }

        [HttpGet("searchByDescricao/{descricao}")]
        public async Task<ActionResult<IEnumerable<TipoAcessorio>>> GetTiposAcessoriosByDescricao(string descricao)
        {
            var tiposAcessorios = await _context.TiposAcessorios
                                                .Where(t => t.Descricao.Contains(descricao))
                                                .ToListAsync();

            if (tiposAcessorios == null || !tiposAcessorios.Any())
            {
                return NotFound();
            }

            return tiposAcessorios;
        }


    }
}
