namespace Plushing.Models
{
    public class Roupa
    {
        public Guid RoupaId { get; set; }
        public decimal Preco { get; set; }
        public string Tipo { get; set; }
        public string Tamanho { get; set; }
        public Guid CorId { get; set; }
        public Cor? Cor { get; set; }
    }
}
