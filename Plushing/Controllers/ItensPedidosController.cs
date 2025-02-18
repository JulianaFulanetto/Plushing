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
    public class ItensPedidosController : ControllerBase
    {
        private readonly PlushingContext _context;

        public ItensPedidosController(PlushingContext context)
        {
            _context = context;
        }

        // GET: api/ItensPedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemPedido>>> GetItensPedido()
        {
            return await _context.ItensPedido.ToListAsync();
        }

        // GET: api/ItensPedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemPedido>> GetItemPedido(Guid id)
        {
            var itemPedido = await _context.ItensPedido.FindAsync(id);

            if (itemPedido == null)
            {
                return NotFound();
            }

            return itemPedido;
        }

        // PUT: api/ItensPedidos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemPedido(Guid id, ItemPedido itemPedido)
        {
            if (id != itemPedido.ItemPedidoId)
            {
                return BadRequest();
            }

            _context.Entry(itemPedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemPedidoExists(id))
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

        // POST: api/ItensPedidos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemPedido>> PostItemPedido(ItemPedido itemPedido)
        {
            _context.ItensPedido.Add(itemPedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemPedido", new { id = itemPedido.ItemPedidoId }, itemPedido);
        }

        // DELETE: api/ItensPedidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemPedido(Guid id)
        {
            var itemPedido = await _context.ItensPedido.FindAsync(id);
            if (itemPedido == null)
            {
                return NotFound();
            }

            _context.ItensPedido.Remove(itemPedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemPedidoExists(Guid id)
        {
            return _context.ItensPedido.Any(e => e.ItemPedidoId == id);
        }

        [HttpPost("calcular-valor/{id}")]
        public async Task<IActionResult> CalcularValorItem(Guid id)
        {
            var itemPedido = await _context.ItensPedido
                .Include(ip => ip.Pelucia)
                .Include(ip => ip.Personalizacao)
                .FirstOrDefaultAsync(ip => ip.ItemPedidoId == id);

            if (itemPedido == null)
            {
                return NotFound();
            }

            var precoBase = itemPedido.Pelucia?.PrecoBase ?? 0;
            var precoPersonalizacao = itemPedido.Personalizacao?.PrecoPersonalizacao ?? 0;
            var precoFinal = (precoBase + precoPersonalizacao) * itemPedido.Quantidade;

            itemPedido.PrecoFinal = precoFinal;

            var carrinho = await _context.Carrinhos
                .FirstOrDefaultAsync(c => c.CarrinhoId == itemPedido.CarrinhoId);

            if (carrinho == null)
            {
                return NotFound();
            }

            carrinho.ValorTotal += precoFinal;

            _context.Entry(itemPedido).State = EntityState.Modified;
            _context.Entry(carrinho).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(new { PrecoFinal = precoFinal, ValorTotalCarrinho = carrinho.ValorTotal });
        }

    }
}
