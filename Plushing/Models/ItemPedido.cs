namespace Plushing.Models
{
    public class ItemPedido
    {
        public Guid ItemPedidoId { get; set; }
        public Guid CarrinhoId { get; set; }
        public Carrinho? Carrinho { get; set; }
        public Guid PeluciaId { get; set; }
        public Pelucia? Pelucia { get; set; }
        public Guid? PersonalizacaoId { get; set; }
        public Personalizacao? Personalizacao { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoFinal { get; set; }
    }
}
