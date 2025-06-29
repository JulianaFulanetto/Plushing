﻿using System;
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
    public class VendasController : ControllerBase
    {
        private readonly PlushingContext _context;

        public VendasController(PlushingContext context)
        {
            _context = context;
        }

        // GET: api/Vendas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVendas()
        {
            return await _context.Vendas.ToListAsync();
        }

        // GET: api/Vendas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Venda>> GetVenda(Guid id)
        {
            var venda = await _context.Vendas.FindAsync(id);

            if (venda == null)
            {
                return NotFound();
            }

            return venda;
        }

        // PUT: api/Vendas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenda(Guid id, Venda venda)
        {
            if (id != venda.VendaId)
            {
                return BadRequest();
            }

            _context.Entry(venda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendaExists(id))
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

        // POST: api/Vendas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Venda>> PostVenda(Venda venda)
        {
            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVenda", new { id = venda.VendaId }, venda);
        }

        // DELETE: api/Vendas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenda(Guid id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }

            _context.Vendas.Remove(venda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendaExists(Guid id)
        {
            return _context.Vendas.Any(e => e.VendaId == id);
        }
        // GET: api/Vendas/formaPagamento/{formaPagamento}
        [HttpGet("formaPagamento/{formaPagamento}")]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVendasByFormaPagamento(string formaPagamento)
        {
            var vendas = await _context.Vendas.Where(v => v.FormaPagamento == formaPagamento).ToListAsync();

            if (vendas == null || !vendas.Any())
            {
                return NotFound();
            }
            return vendas;
        }

        // GET: api/Vendas/data/{data}
        [HttpGet("data/{data}")]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVendasByData(DateTime data)
        {
            var vendas = await _context.Vendas.Where(v => v.DataVenda.Date == data.Date).ToListAsync();

            if (vendas == null || !vendas.Any())
            {
                return NotFound();
            }
            return vendas;
        }

        // GET: api/Vendas/intervaloDatas?dataInicio={dataInicio}&dataFim={dataFim}
        [HttpGet("intervaloDatas")]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVendasByIntervaloDatas(DateTime dataInicio, DateTime dataFim)
        {
            var vendas = await _context.Vendas
                .Where(v => v.DataVenda.Date >= dataInicio.Date && v.DataVenda.Date <= dataFim.Date)
                .ToListAsync();

            if (vendas == null || !vendas.Any())
            {
                return NotFound();
            }

            return vendas;
        }

        // GET: api/Vendas/cliente/{clienteId}/data/{data}
        [HttpGet("cliente/{clienteId}/data/{data}")]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVendasByClienteEData(Guid clienteId, DateTime data)
        {
            var vendas = await _context.Vendas
                .Include(v => v.Carrinho)
                .Where(v => v.Carrinho.ClienteId == clienteId && v.DataVenda.Date == data.Date)
                .ToListAsync();

            if (vendas == null || !vendas.Any())
            {
                return NotFound();
            }

            return vendas;
        }

        // GET: api/Vendas/cliente/{clienteId}/formaPagamento/{formaPagamento}
        [HttpGet("cliente/{clienteId}/formaPagamento/{formaPagamento}")]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVendasByClienteEFormaPagamento(Guid clienteId, string formaPagamento)
        {
            var vendas = await _context.Vendas
                .Include(v => v.Carrinho)
                .Where(v => v.Carrinho.ClienteId == clienteId && v.FormaPagamento == formaPagamento)
                .ToListAsync();

            if (vendas == null || !vendas.Any())
            {
                return NotFound();
            }

            return vendas;
        }

        //GET: Retorne todas as vendas de um cliente específico
        [HttpGet("cliente/{clienteId}")]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVendasByCliente(Guid clienteId)
        {
            var vendas = await _context.Vendas
                .Include(v => v.Carrinho)
                .Where(v => v.Carrinho.ClienteId == clienteId)
                .ToListAsync();
            if (vendas == null || !vendas.Any())
            {
                return NotFound();
            }
            return vendas;
        }
    }
}
