using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Domain.Models;

namespace Questao5.Infrastructure.Database.Repositories
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        IConfiguration _config;
        IContaCorrenteQueryBuilderService _contaCorrenteQueryBuilder;
        public ContaCorrenteRepository(IConfiguration config, IContaCorrenteQueryBuilderService contaCorrenteQueryBuilder)
        {
            _config = config;
            _contaCorrenteQueryBuilder = contaCorrenteQueryBuilder;
        }

        public string GetConnectionString()
        {
            return _config.GetSection("Infrastructure").GetSection("ConnectionStrings").Value;
        }

        public async Task<ContaCorrente> SelectContaCorrente(string idContaCorrente)
        {
            var connectionString = GetConnectionString();

            var result = new ContaCorrente();

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                var insertMovimentoQuery = _contaCorrenteQueryBuilder.CreateSelectQuery(idContaCorrente);

                result = await con.QueryFirstOrDefaultAsync<ContaCorrente>(insertMovimentoQuery.RawSql, insertMovimentoQuery.Parameters);

                con.Close();
            }

            return result;
        }
    }
}
