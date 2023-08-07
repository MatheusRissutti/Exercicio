using Questao5.Domain.Models;
using Questao5.Infrastructure.Services;
using System.Threading.Tasks;

namespace Questao5.Domain.Interfaces
{
    public interface IContaCorrenteService
    {
        Task<GetContaCorrenteModel> GetContaCorrente(string idContaCorrente);

        Task<Result<decimal>> GetSaldoContaCorrente(string idContaCorrente);
    }
}
