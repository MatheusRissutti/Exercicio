using Questao5.Domain.Entities;
using Questao5.Domain.Models;

namespace Questao5.Domain.Interfaces
{
    public interface IMovimentoRepository
    {
        Task<string> InsertMovimento(CreateMovimentoModel model);
        Task<Movimento> SelectMovimento(string requisicao);
        Task<List<Movimento>> SelectMovimentoList(string requisicao);
    }
}
