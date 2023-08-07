using static Questao5.Domain.Enumerators.EnumContaCorrente;

namespace Questao5.Domain.Models
{
    public class CreateMovimentoModel
    {
        public Guid IdRequisicao { get; set; }
        public string IdContaCorrente { get; set; }
        public decimal Valor { get; set; }
        public string TipoMovimentacao { get; set; }
    }
}