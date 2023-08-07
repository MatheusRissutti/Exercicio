using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Services;

namespace Questao5.Useful
{
    public class ValidationsCommon : IValidationsCommon
    {
        IContaCorrenteRepository _contaCorrenteRepository;
        public ValidationsCommon(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<Result<bool>> ContaCorrenteValidation(string idContaCorrente)
        {
            var result = await _contaCorrenteRepository.SelectContaCorrente(idContaCorrente);

            if (result == null)
                return Result<bool>.WithError(false, ResultEnum.INVALID_ACCOUNT);

            if (result.Ativo == 0)
                return Result<bool>.WithError(false, ResultEnum.INACTIVE_ACCOUNT);

            return Result<bool>.WithSuccess(true); 
        }
    }
}
