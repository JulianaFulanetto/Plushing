namespace Plushing.Models
{
    public class Carrinho
    {
        public Guid CarrinhoId { get; set; }
        public Guid ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public List<ItemPedido> Itens { get; set; }
    }
}
