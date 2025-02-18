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
    public class PersonalizacoesController : ControllerBase
    {
        private readonly PlushingContext _context;

        public PersonalizacoesController(PlushingContext context)
        {
            _context = context;
        }

        // GET: api/Personalizacoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personalizacao>>> GetPersonalizacoes()
        {
            return await _context.Personalizacoes.ToListAsync();
        }

        // GET: api/Personalizacoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Personalizacao>> GetPersonalizacao(Guid id)
        {
            var personalizacao = await _context.Personalizacoes.FindAsync(id);

            if (personalizacao == null)
            {
                return NotFound();
            }

            return personalizacao;
        }

        // PUT: api/Personalizacoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonalizacao(Guid id, Personalizacao personalizacao)
        {
            if (id != personalizacao.PersonalizacaoId)
            {
                return BadRequest();
            }

            _context.Entry(personalizacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalizacaoExists(id))
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

        // POST: api/Personalizacoes
        [HttpPost]
        public async Task<ActionResult<Personalizacao>> PostPersonalizacao(Personalizacao personalizacao)
        {
            _context.Personalizacoes.Add(personalizacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonalizacao", new { id = personalizacao.PersonalizacaoId }, personalizacao);
        }

        // DELETE: api/Personalizacoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonalizacao(Guid id)
        {
            var personalizacao = await _context.Personalizacoes.FindAsync(id);
            if (personalizacao == null)
            {
                return NotFound();
            }

            _context.Personalizacoes.Remove(personalizacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Personalizacoes/searchByPresenteado/{name}
        [HttpGet("searchByPresenteado/{name}")]
        public async Task<ActionResult<IEnumerable<Personalizacao>>> GetPersonalizacoesByPresenteado(string name)
        {
            var personalizacoes = await _context.Personalizacoes
                                                .Where(p => p.Presenteado.Contains(name))
                                                .ToListAsync();

            if (personalizacoes == null || !personalizacoes.Any())
            {
                return NotFound();
            }

            return personalizacoes;
        }

        // GET: api/Personalizacoes/searchByPriceRange
        [HttpGet("searchByPriceRange")]
        public async Task<ActionResult<IEnumerable<Personalizacao>>> GetPersonalizacoesByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var personalizacoes = await _context.Personalizacoes
                                                .Where(p => p.PrecoPersonalizacao >= minPrice && p.PrecoPersonalizacao <= maxPrice)
                                                .ToListAsync();

            if (personalizacoes == null || !personalizacoes.Any())
            {
                return NotFound();
            }

            return personalizacoes;
        }

        private bool PersonalizacaoExists(Guid id)
        {
            return _context.Personalizacoes.Any(e => e.PersonalizacaoId == id);
        }
    }
}
