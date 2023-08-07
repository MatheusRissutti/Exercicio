using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using System.Data.SqlClient;

namespace Questao5.Infrastructure.Database.Repositories
{
    public class IdempotenciaRepository : IIdempotenciaRepository
    {
        IConfiguration _config;
        IIdempotenciaQueryBuilderService _queryBuilder;
        public IdempotenciaRepository(IConfiguration config, IIdempotenciaQueryBuilderService queryBuilder)
        {
            _config = config;
            _queryBuilder = queryBuilder;
        }

        public string GetConnectionString()
        {
            return _config.GetSection("Infrastructure").GetSection("ConnectionStrings").Value;
        }

        public async Task<Idempotencia> SelectIdempotencia(Guid idRequisicao)
        {
            var connectionString = GetConnectionString();

            var result = new Idempotencia();

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                var query = _queryBuilder.CreateSelectResultadoQuery(idRequisicao);

                result = await con.QueryFirstOrDefaultAsync<Idempotencia>(query.RawSql, query.Parameters);

                con.Close();
            }

            return result;
        }
    }
}
