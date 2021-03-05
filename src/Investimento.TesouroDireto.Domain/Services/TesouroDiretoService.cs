using Investimento.TesouroDireto.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Investimento.TesouroDireto.Domain.Services
{
    public class TesouroDiretoService : ITesouroDiretoService
    {
        private readonly IB3TesouroDiretoService _b3TesouroDiretoService;

        public TesouroDiretoService(IB3TesouroDiretoService b3TesouroDiretoService)
        {
            _b3TesouroDiretoService = b3TesouroDiretoService;
        }

        public async Task<List<Entities.TesouroDireto>> GetAllByAccountIdAsync(long accountId)
        {
            return await _b3TesouroDiretoService.GetAllByAccountIdAsync(accountId);
        }
    }
}
