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

        public DbSet<Roupa> Roupas { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Cliente>().ToTable("Cliente");
            builder.Entity<Carrinho>().ToTable("Carrinho");
            builder.Entity<ItemPedido>().ToTable("ItemPedido");
            builder.Entity<Venda>().ToTable("Venda");
            builder.Entity<Pelucia>().ToTable("Pelucia");
            builder.Entity<Personalizacao>().ToTable("Personalizacao");
            builder.Entity<Cor>().ToTable("Cor");
            builder.Entity<Roupa>().ToTable("Roupa");
        }
    }
}
