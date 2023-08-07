using Questao5.Infrastructure.Services;

namespace Questao5.Domain.Interfaces
{
    public interface IValidationsCommon
    {
        Task<Result<bool>> ContaCorrenteValidation(string idContaCorrente);
    }
}
