using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;
using Questao5.Domain.Models;
using Questao5.Useful;
using static Questao5.Domain.Enumerators.EnumContaCorrente;

namespace Questao5.Infrastructure.Services
{
    public class MovimentoService : IMovimentoService
    {
        IMovimentoRepository _movimentoRepository;
        IIdempotenciaRepository _idempotenciaRepository;
        IValidationsCommon _validations;
        public MovimentoService(IMovimentoRepository movimentoRepository, IIdempotenciaRepository idempotenciaRepository, IValidationsCommon validations)
        {
            _movimentoRepository = movimentoRepository;
            _idempotenciaRepository = idempotenciaRepository;
            _validations = validations;
        }

        public async Task<Result<string>> CreateMovimento(CreateMovimentoModel model)
        {
            var validation = await IsMovimentacaoValid(model);

            if (!validation.Success)
                return Result<string>.WithError("Couldn`t validate request.", validation.ResultEnum);

            var idempotencia = await _idempotenciaRepository.SelectIdempotencia(model.IdRequisicao);

            if (idempotencia != null && idempotencia.Resultado.Equals("Success"))
            {
                var movimento = await _movimentoRepository.SelectMovimento(idempotencia.Requisicao);
                return Result<string>.WithSuccess(movimento.IdMovimento);
            }

            var idMovimento = await _movimentoRepository.InsertMovimento(model);

            return Result<string>.WithSuccess(idMovimento);
        }

        private async Task<Result<bool>> IsMovimentacaoValid(CreateMovimentoModel model)
        {
            if (model.Valor <= 0)
                return Result<bool>.WithError(false, ResultEnum.INVALID_VALUE);

            if (!TipoContaCorrenteEnum.TryParse(model.TipoMovimentacao, out TipoContaCorrenteEnum enumConverted ))
                return Result<bool>.WithError(false, ResultEnum.INVALID_TYPE);

            var validation = await _validations.ContaCorrenteValidation(model.IdContaCorrente);

            if (!validation.Success)
                return validation;

            return Result<bool>.WithSuccess(true);
        }
    }
}
