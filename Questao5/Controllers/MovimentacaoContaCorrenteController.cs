using FluentAssertions.Common;
using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;
using Questao5.Domain.Models;
using System.Xml.Linq;

namespace Questao5.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MovimentacaoContaCorrenteController : ControllerBase
    {
        IMovimentoService _movimentoService;
        public MovimentacaoContaCorrenteController(IMovimentoService movimentoService )
        {
            _movimentoService = movimentoService;
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultEnum), StatusCodes.Status400BadRequest)]
        [HttpPost(Name = "MovimentaContaCorrente")]
        public async Task<ActionResult<GetContaCorrenteModel>> PostMovimentacao(CreateMovimentoModel model)
        {

            var result = await _movimentoService.CreateMovimento(model);

            if (!result.Success)
                return BadRequest(result.ResultEnum);

            return Ok(result);
        }
    }
}
