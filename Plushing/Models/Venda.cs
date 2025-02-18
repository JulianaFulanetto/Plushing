namespace Plushing.Models
{
    public class Venda
    {
        public Guid VendaId { get; set; }
        public Guid CarrinhoId { get; set; }
        public Carrinho? Carrinho { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorTotal { get; set; }
        public string FormaPagamento { get; set; }

    }
}
