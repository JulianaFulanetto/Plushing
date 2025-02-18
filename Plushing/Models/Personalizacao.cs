using System.Reflection.Metadata;

namespace Plushing.Models
{
    public class Personalizacao
    {
        public Guid PersonalizacaoId { get; set; }
        public Guid CorId { get; set; }
        public Cor? Cor { get; set; }
        public string Presenteado { get; set; }
        public string NomePelucia { get; set; }

    }
}
