using System.Reflection.Metadata;

namespace Plushing.Models
{
    public class Personalizacao
    {
        public Guid PersonalizacaoId { get; set; }
        public Guid PeluciaId { get; set; }
        public Pelucia? Pelucia { get; set; }
        public Guid CorId { get; set; }
        public Cor? Cor { get; set; }
        public string TextoPersonalizado { get; set; }

    }
}
