using System.Collections.Generic;
using System.Threading.Tasks;

namespace Investimento.TesouroDireto.Domain.Interfaces.Services
{
    public interface ITesouroDiretoService
    {
        Task<List<Entities.TesouroDireto>> GetAllByAccountIdAsync(long accountId);
    }
}
