using Investimento.TesouroDireto.Domain.Extensions;
using Investimento.TesouroDireto.Domain.Interfaces.Services;
using Investimento.TesouroDireto.Infrastructure.ViewModels;
using KissLog;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Investimento.TesouroDireto.Infrastructure.Services
{
    public class B3TesouroDiretoService : IB3TesouroDiretoService
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public B3TesouroDiretoService(IHttpClientFactory httpFactory, ILogger logger, IConfiguration configuration)
        {
            _httpFactory = httpFactory;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<List<Domain.Entities.TesouroDireto>> GetAllByAccountIdAsync(long accountId)
        {
            try
            {
                var listTesourosDiretos = new List<Domain.Entities.TesouroDireto>();
                var jsonString = await ConsumeEndpoint(accountId);
                var items = jsonString.JsonGetByName("tds");

                if (string.IsNullOrEmpty(items))
                    return listTesourosDiretos;

                var tesourosDiretosViewModel = JsonConvert.DeserializeObject<List<TesouroDiretoViewModel>>(items);
                tesourosDiretosViewModel?.ForEach(x => listTesourosDiretos.Add(new Domain.Entities.TesouroDireto(x.ValorInvestido, x.ValorTotal, x.Vencimento, x.DataDeCompra, x.Iof, x.Indice, x.Tipo, x.Nome)));
                return listTesourosDiretos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> ConsumeEndpoint(long accountId)
        {
            try
            {
                using (HttpClient httpclient = _httpFactory.CreateClient("mockinvestimento"))
                using (HttpResponseMessage httpResponse = await httpclient.GetAsync(_configuration.GetSection("AppSettings:Mockyio:RequestUri")?.Value))
                {
                    var body = await httpResponse.Content?.ReadAsStringAsync();

                    _logger.Trace("RequestUrl: " + httpResponse.RequestMessage.RequestUri?.ToString() +
                                "\nMethod: " + httpResponse.RequestMessage.Method?.ToString() +
                                "\nResponseStatusCode: " + httpResponse?.StatusCode +
                                "\nResponseBody: " + body, "ConsomeEndpoint", 20);

                    return !string.IsNullOrWhiteSpace(body) ? body : null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
