using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Plushing.Models;

namespace Plushing.Data
{
    public class PlushingContext : IdentityDbContext
    {
        public PlushingContext(DbContextOptions<PlushingContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Pelucia> Pelucias { get; set; }
        public DbSet<Personalizacao> Personalizacoes { get; set; }
        public DbSet<Cor> Cores { get; set; }
        public DbSet<Acessorio> Acessorios { get; set; }
        public DbSet<Padrao> Padroes { get; set; }
        public DbSet<Tamanho> Tamanhos { get; set; }
        public DbSet<TipoAcessorio> TiposAcessorios { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Cliente>().ToTable("Cliente");
            builder.Entity<Carrinho>().ToTable("Carrinho");
            builder.Entity<ItemPedido>().ToTable("ItemPedido");
            builder.Entity<Venda>().ToTable("Venda");
            builder.Entity<Pelucia>().ToTable("Pelucia");
            builder.Entity<Personalizacao>().ToTable("Personalizacao");
            builder.Entity<Acessorio>().ToTable("Acessorio");
            builder.Entity<Padrao>().ToTable("Padrao");
            builder.Entity<Cor>().ToTable("Cor");
            builder.Entity<Tamanho>().ToTable("Tamanho");
            builder.Entity<TipoAcessorio>().ToTable("TipoAcessorio");

        }
    }
}
