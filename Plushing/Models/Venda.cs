namespace Plushing.Models
{
    public class Venda
    {
        public Guid VendaId { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorTotal { get; set; }
        public Guid ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
