using Dapper;
using Questao5.Domain.Interfaces;
using Questao5.Domain.Models;
using System.Data;

namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
    public class MovimentoQueryBuilderService : IMovimentoQueryBuilderService
    {
        public SqlBuilder.Template CreateInsertQuery(CreateMovimentoModel model)
        {
            var builder = new SqlBuilder();

            var queryBase = @"INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)
                              OUTPUT INSERTED.idmovimento
                              VALUES(NEWID(), @idcontacorrente, GETDATE(), @tipomovimento, @valor)";

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@idcontacorrente", model.IdContaCorrente, DbType.String, ParameterDirection.Input);
            parameters.Add("@tipomovimento", model.TipoMovimentacao.ToString(), DbType.String, ParameterDirection.Input);
            parameters.Add("@valor", model.Valor, DbType.Decimal, ParameterDirection.Input);

            builder.AddParameters(parameters);

            return builder.AddTemplate(queryBase);
        }

        public SqlBuilder.Template CreateSelectQueryById(string idMovimento)
        {
            var builder = new SqlBuilder();

            var queryBase = @"SELECT idmovimento, idcontacorrente, datamovimento, tipomovimento, valor
                              FROM movimento
                              WHERE idmovimento = @idMovimento";

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@idMovimento", idMovimento, DbType.String, ParameterDirection.Input);

            builder.AddParameters(parameters);

            return builder.AddTemplate(queryBase);
        }

        public SqlBuilder.Template CreateSelectQueryByContaCorrente(string idContaCorrente)
        {
            var builder = new SqlBuilder();

            var queryBase = @"SELECT idmovimento, idcontacorrente, datamovimento, tipomovimento, valor
                              FROM movimento
                              WHERE idcontacorrente = @idContaCorrente";

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@idContaCorrente", idContaCorrente, DbType.String, ParameterDirection.Input);

            builder.AddParameters(parameters);

            return builder.AddTemplate(queryBase);
        }

    }
}
