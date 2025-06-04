using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plushing.Data;
using Plushing.Models;

namespace Plushing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcoesController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly PlushingContext _context;

        public AcoesController(UserManager<IdentityUser> userManager, PlushingContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // Implementar um ENdpoint que receba o e-mail do usuario, busque a Identidade e Desbloqueie o Usuario
        [HttpPost("desbloquear")]
        public async Task<IActionResult> DesbloquearUsuario([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }
            user.LockoutEnd = null; // Remove o bloqueio
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok("Usuário desbloqueado com sucesso.");
            }
            return BadRequest("Erro ao desbloquear o usuário: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }


        // Implementar um Enpoint que recebe o e-mail, busca a identidade e em seguida, verifica se esse IdentityUser esta vicunlado a algum registro na tabela de Clientes
        [HttpGet("verificar-cliente/{email}")]
        public async Task<IActionResult> VerificarCliente(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Verificar se exite um cliente atrelado ao usuário
            var cliente = await _context.Clientes.Where(c => c.UserId.ToString() == user.Id).FirstOrDefaultAsync();
            if (cliente == null)
            {
                return NoContent();
            }
            return Ok(cliente);
        }

        // Implementear um Enpoint /atualizar-cliente que vai criar um Usuario viculado ao Identity
        [HttpPut("cadastar-perfil/{email}")]
        public async Task<IActionResult> CriarPerfil(string email, Cliente NovoCliente)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }
            var cliente = await _context.Clientes.Where(c => c.UserId.ToString() == user.Id).FirstOrDefaultAsync();
            if (cliente != null)
            {
                cliente.Telefone = NovoCliente.Telefone;
                cliente.Nome = NovoCliente.Nome;
                cliente.Cpf = NovoCliente.Cpf;
                _context.Clientes.Update(cliente);
                await _context.SaveChangesAsync();
                return Ok(cliente);
            }

            NovoCliente.UserId = Guid.Parse(user.Id);
            NovoCliente.User = user;

            _context.Clientes.Add(NovoCliente);
            await _context.SaveChangesAsync();

            return Ok(NovoCliente);


        }
    }
}
