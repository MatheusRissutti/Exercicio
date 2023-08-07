using Dapper;
using Questao5.Domain.Models;

namespace Questao5.Domain.Interfaces
{
    public interface IContaCorrenteQueryBuilderService
    {
        SqlBuilder.Template CreateSelectQuery(string idContaCorrente);
    }
}
