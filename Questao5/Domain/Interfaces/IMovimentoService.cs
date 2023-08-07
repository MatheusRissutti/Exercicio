using Questao5.Domain.Models;
using Questao5.Infrastructure.Services;

namespace Questao5.Domain.Interfaces
{
    public interface IMovimentoService
    {
        Task<Result<string>> CreateMovimento(CreateMovimentoModel model);
    }
}
