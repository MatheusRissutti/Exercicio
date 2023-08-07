using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;
using Questao5.Domain.Models;

namespace Questao5.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ContaCorrenteController : ControllerBase
    {
        IContaCorrenteService _contaCorrenteService;
        public ContaCorrenteController(IContaCorrenteService contaCorrenteService)
        {
            _contaCorrenteService = contaCorrenteService;
        }

        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultEnum), StatusCodes.Status400BadRequest)]
        [HttpGet(Name = "GetSaldo")]
        public async Task<ActionResult<GetContaCorrenteModel>> GetSaldo(string contaCorrenteId)
        {
            var result = await _contaCorrenteService.GetSaldoContaCorrente(contaCorrenteId);

            if (!result.Success)
                return BadRequest(result.ResultEnum);

            return Ok(result);
        }
    }
}
