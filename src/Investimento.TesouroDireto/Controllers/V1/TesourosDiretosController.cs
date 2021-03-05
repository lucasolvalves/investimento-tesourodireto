using Investimento.TesouroDireto.Domain.Interfaces.Services;
using KissLog;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Investimento.TesouroDireto.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/tesouros_diretos")]
    public class TesourosDiretosController : MainController
    {
        private readonly ITesouroDiretoService _tesouroDiretoService;

        public TesourosDiretosController(ILogger logger, ITesouroDiretoService tesouroDiretoService) : base(logger)
        {
            _tesouroDiretoService = tesouroDiretoService;
        }

        [HttpGet("{accountId:long}")]
        public async Task<IActionResult> GetInvestments(long accountId)
        {
            var clienteInvestimentosViewModel = await _tesouroDiretoService.GetAllByAccountIdAsync(accountId);

            if (clienteInvestimentosViewModel?.Count == 0)
                return NotFound();

            return Ok(new { data = clienteInvestimentosViewModel });
        }
    }
}
