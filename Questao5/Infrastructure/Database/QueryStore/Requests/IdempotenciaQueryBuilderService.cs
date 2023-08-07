using Dapper;
using Questao5.Domain.Interfaces;
using Questao5.Domain.Models;
using System.Data;
using System.Reflection.Metadata;

namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
    public class IdempotenciaQueryBuilderService : IIdempotenciaQueryBuilderService
    {

        public SqlBuilder.Template CreateInsertQuery(Guid idRequisicao, string idMovimento)
        {
            var builder = new SqlBuilder();

            var queryBase = @"INSERT INTO Idempotencia (chave_idempotencia, requisicao, resultado)
                           VALUES (@chave_idempotencia, @requisicao, @resultado)";

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@chave_idempotencia", idRequisicao, DbType.Guid, ParameterDirection.Input);
            parameters.Add("@requisicao", idMovimento, DbType.String, ParameterDirection.Input);
            parameters.Add("@resultado", "Success", DbType.String, ParameterDirection.Input);

            builder.AddParameters(parameters);

            return builder.AddTemplate(queryBase);
        }

        public SqlBuilder.Template CreateSelectResultadoQuery(Guid idRequisicao)
        {
            var builder = new SqlBuilder();

            var queryBase = @"SELECT chave_idempotencia, requisicao, resultado
                              FROM Idempotencia
                              WHERE chave_idempotencia = @idRequisicao";

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@idRequisicao", idRequisicao, DbType.Guid, ParameterDirection.Input);

            builder.AddParameters(parameters);

            return builder.AddTemplate(queryBase);
        }
    }
}
