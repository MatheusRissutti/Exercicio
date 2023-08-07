using Questao5.Domain.Entities;
using Questao5.Domain.Models;

namespace Questao5.Domain.Interfaces
{
    public interface IContaCorrenteRepository
    {
        Task<ContaCorrente> SelectContaCorrente(string idContaCorrente);
    }
}
