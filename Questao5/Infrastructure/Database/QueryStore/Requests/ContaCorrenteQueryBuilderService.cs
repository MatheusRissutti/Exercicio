using Dapper;
using Newtonsoft.Json.Linq;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Domain.Models;
using System.Data;

namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
    public class ContaCorrenteQueryBuilderService : IContaCorrenteQueryBuilderService
    {
        public SqlBuilder.Template CreateSelectQuery(string idContaCorrente)
        {
            var builder = new SqlBuilder();

            var queryBase = @"SELECT idcontacorrente, numero, nome, ativo
                              FROM contacorrente
                              WHERE idcontacorrente = @idcontacorrente";

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@idcontacorrente", idContaCorrente, DbType.String, ParameterDirection.Input);

            builder.AddParameters(parameters);

            return builder.AddTemplate(queryBase);
        }
    }
}
