namespace Plushing.Models
{
    public class Carrinho
    {
        public Guid CarrinhoId { get; set; }
        public Guid ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public DateTime DataCriacao { get; set; }
        public int NumeroItens { get; set; }
        public int NumeroPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public bool Finalizado { get; set; } = false;
        public bool Cancelado { get; set; } = false;

    }
}
