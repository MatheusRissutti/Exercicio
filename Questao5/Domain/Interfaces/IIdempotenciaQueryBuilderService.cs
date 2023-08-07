using Dapper;
using Questao5.Domain.Models;

namespace Questao5.Domain.Interfaces
{
    public interface IIdempotenciaQueryBuilderService
    {
        SqlBuilder.Template CreateInsertQuery(Guid idRequisicao, string idMovimento);
        SqlBuilder.Template CreateSelectResultadoQuery(Guid idRequisicao);
    }
}
