using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;
using Questao5.Domain.Models;
using static Dapper.SqlMapper;
using System.Linq;

namespace Questao5.Infrastructure.Services
{
    public class ContaCorrenteService : IContaCorrenteService
    {
        IValidationsCommon _validations;
        IContaCorrenteRepository _repositoryContaCorrente;
        IMovimentoRepository _movimentoRepository;
        public ContaCorrenteService(IContaCorrenteRepository repository, IMovimentoRepository movimentoRepository, IValidationsCommon validations)
        {
            _repositoryContaCorrente = repository;
            _movimentoRepository = movimentoRepository;
            _validations = validations;

        }

        public async Task<GetContaCorrenteModel> GetContaCorrente(string idContaCorrente)
        {
            var result = await _repositoryContaCorrente.SelectContaCorrente(idContaCorrente);

            return result == null ? null : ContaCorrenteModelMapper(result);
        }

        public async Task<Result<decimal>> GetSaldoContaCorrente(string idContaCorrente)
        {
            var validation = await _validations.ContaCorrenteValidation(idContaCorrente);

            if (!validation.Success)
                return Result<decimal>.WithError(0, validation.ResultEnum);

            var result = await _movimentoRepository.SelectMovimentoList(idContaCorrente);

            decimal saldo = 0;

            foreach(var movimento in result)
            {
                if (movimento.TipoMovimento.Equals("C"))
                    saldo += movimento.Valor;
                if (movimento.TipoMovimento.Equals("D"))
                    saldo -= movimento.Valor;
            }

            return Result<decimal>.WithSuccess(saldo);
        }


        private GetContaCorrenteModel ContaCorrenteModelMapper(ContaCorrente entity)
        {
            return new GetContaCorrenteModel() { Ativo = entity.Ativo, Nome = entity.Nome, Numero = entity.Numero };
        }
    }
}
