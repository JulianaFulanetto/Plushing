using System.Reflection.Metadata;

namespace Plushing.Models
{
    public class Personalizacao
    {
        public Guid PersonalizacaoId { get; set; }
        public Guid CorId { get; set; }
        public Cor? Cor { get; set; }
        public Guid PadraoId { get; set; }
        public Padrao? Padrao { get; set; }
        public Guid TamanhoId { get; set; }
        public Tamanho? Tamanho { get; set; }
        public Guid AcessorioId { get; set; }
        public Acessorio? Acessorio { get; set; }
        public decimal PrecoPersonalizacao { get; set; }

        public string Presenteado { get; set; }
        public string NomePelucia { get; set; }

    }
}
