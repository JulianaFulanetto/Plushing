namespace Plushing.Models
{
    public class Acessorio
    {
        public Guid AcessorioId { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public Guid TipoAcessorioId { get; set; }
        public TipoAcessorio? TipoAcessorio { get; set; }
    }
}
