using Dapper;
using Questao5.Domain.Models;

namespace Questao5.Domain.Interfaces
{
    public interface IMovimentoQueryBuilderService
    {
        SqlBuilder.Template CreateInsertQuery(CreateMovimentoModel model);
        SqlBuilder.Template CreateSelectQueryById(string idMovimento);
        SqlBuilder.Template CreateSelectQueryByContaCorrente(string idContaCorrente);
    }
}
