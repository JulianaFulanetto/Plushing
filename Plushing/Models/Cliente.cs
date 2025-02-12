using Microsoft.AspNetCore.Identity;

namespace Plushing.Models
{
    public class Cliente
    {
        public Guid ClienteId { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }

        public Guid? UserId { get; set; }
        public IdentityUser? User { get; set; }

    }
}
