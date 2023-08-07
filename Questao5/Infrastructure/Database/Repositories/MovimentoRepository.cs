using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Domain.Models;
using System.Data.SqlClient;

namespace Questao5.Infrastructure.Database.Repositories
{
    public class MovimentoRepository : IMovimentoRepository
    {
        IConfiguration _config;
        IMovimentoQueryBuilderService _movimentoQueryBuilder;
        IIdempotenciaQueryBuilderService _idempotenciaQueryBuilder;
        public MovimentoRepository(IConfiguration config, IMovimentoQueryBuilderService movimentoQueryBuilder, IIdempotenciaQueryBuilderService idempotenciaQueryBuilder)
        {
            _config = config;
            _movimentoQueryBuilder = movimentoQueryBuilder;
            _idempotenciaQueryBuilder = idempotenciaQueryBuilder;
        }

        public string GetConnectionString()
        {
            return _config.GetSection("Infrastructure").GetSection("ConnectionStrings").Value;
        }

        public async Task<string> InsertMovimento(CreateMovimentoModel model)
        {
            var connectionString = GetConnectionString();

            string idMovimento = string.Empty;

            using (var con = new SqlConnection(connectionString))
            {
                await con.OpenAsync();

                using (var transaction = con.BeginTransaction())
                {
                    var insertMovimentoQuery = _movimentoQueryBuilder.CreateInsertQuery(model);

                    idMovimento = await con.ExecuteScalarAsync<string>(insertMovimentoQuery.RawSql, insertMovimentoQuery.Parameters, transaction);

                    var insertIdempotenciaQuery = _idempotenciaQueryBuilder.CreateInsertQuery(model.IdRequisicao, idMovimento);
                    await con.ExecuteAsync(insertIdempotenciaQuery.RawSql, insertIdempotenciaQuery.Parameters, transaction);

                    if (!string.IsNullOrEmpty(idMovimento))
                        await transaction.CommitAsync();

                    transaction.Dispose();

                    con.Close();
                }
            }

            return idMovimento;
        }

        public async Task<Movimento> SelectMovimento(string requisicao)
        {
            var connectionString = GetConnectionString();

            var movimento = new Movimento();

            using (var con = new SqlConnection(connectionString))
            {
                await con.OpenAsync();

                var query = _movimentoQueryBuilder.CreateSelectQueryById(requisicao);

                movimento = await con.QueryFirstOrDefaultAsync<Movimento>(query.RawSql, query.Parameters);

                con.Close();
            }

            return movimento;
        }

        public async Task<List<Movimento>> SelectMovimentoList(string contaCorrenteId)
        {
            var connectionString = GetConnectionString();

            var movimento = new List<Movimento>();

            using (var con = new SqlConnection(connectionString))
            {
                await con.OpenAsync();

                var query = _movimentoQueryBuilder.CreateSelectQueryByContaCorrente(contaCorrenteId);

                var result = await con.QueryAsync<Movimento>(query.RawSql, query.Parameters);
                movimento.AddRange(result);

                con.Close();
            }

            return movimento;
        }
    }
}
