namespace Plushing.Models
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public Guid VendaId { get; set; }
        public Venda? Venda { get; set; }
        public Guid PeluciaId { get; set; }
        public Pelucia? Pelucia { get; set; }
        public Guid? PersonalizacaoId { get; set; }
        public Personalizacao? Personalizacao { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoFinal { get; set; }
    }
}
