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
    public class PeluciasController : ControllerBase
    {
        private readonly PlushingContext _context;

        public PeluciasController(PlushingContext context)
        {
            _context = context;
        }

        // GET: api/Pelucias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pelucia>>> GetPelucias()
        {
            return await _context.Pelucias.ToListAsync();
        }

        // GET: api/Pelucias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pelucia>> GetPelucia(Guid id)
        {
            var pelucia = await _context.Pelucias.FindAsync(id);

            if (pelucia == null)
            {
                return NotFound();
            }

            return pelucia;
        }

        // PUT: api/Pelucias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPelucia(Guid id, Pelucia pelucia)
        {
            if (id != pelucia.PeluciaId)
            {
                return BadRequest();
            }

            _context.Entry(pelucia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeluciaExists(id))
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

        // POST: api/Pelucias
        [HttpPost]
        public async Task<ActionResult<Pelucia>> PostPelucia(Pelucia pelucia)
        {
            _context.Pelucias.Add(pelucia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPelucia", new { id = pelucia.PeluciaId }, pelucia);
        }

        // DELETE: api/Pelucias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePelucia(Guid id)
        {
            var pelucia = await _context.Pelucias.FindAsync(id);
            if (pelucia == null)
            {
                return NotFound();
            }

            _context.Pelucias.Remove(pelucia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Pelucias/searchByName/{name}
        [HttpGet("searchByName/{name}")]
        public async Task<ActionResult<IEnumerable<Pelucia>>> GetPeluciasByName(string name)
        {
            var pelucias = await _context.Pelucias
                                         .Where(p => p.Nome.Contains(name))
                                         .ToListAsync();

            if (pelucias == null || !pelucias.Any())
            {
                return NotFound();
            }

            return pelucias;
        }

        // GET: api/Pelucias/searchByPriceRange
        [HttpGet("searchByPriceRange")]
        public async Task<ActionResult<IEnumerable<Pelucia>>> GetPeluciasByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var pelucias = await _context.Pelucias
                                         .Where(p => p.PrecoBase >= minPrice && p.PrecoBase <= maxPrice)
                                         .ToListAsync();

            if (pelucias == null || !pelucias.Any())
            {
                return NotFound();
            }

            return pelucias;
        }

        private bool PeluciaExists(Guid id)
        {
            return _context.Pelucias.Any(e => e.PeluciaId == id);
        }
    }
}
