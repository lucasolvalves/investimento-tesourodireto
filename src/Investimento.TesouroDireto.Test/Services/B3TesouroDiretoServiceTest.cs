using Investimento.TesouroDireto.Infrastructure.Services;
using Investimento.TesouroDireto.Test.Builders;
using KissLog;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Investimento.TesouroDireto.Test.Services
{
    [TestClass]
    public class B3TesouroDiretoServiceTest
    {
        private Mock<IHttpClientFactory> _httpFactory;
        private Mock<ILogger> _logger;
        private Mock<IConfigurationSection> _configurationSection;
        private const string JsonSucess = "{\r\n\"tds\": [\r\n{\r\n\t\t\t\"valorInvestido\": 799.4720,\r\n\t\t\t\"valorTotal\": 829.68,\r\n\t\t\t\"vencimento\": \"2025-03-01T00:00:00\",\r\n\t\t\t\"dataDeCompra\": \"2015-03-01T00:00:00\",\r\n\t\t\t\"iof\": 0,\r\n\t\t\t\"indice\": \"SELIC\",\r\n\t\t\t\"tipo\": \"TD\",\r\n\t\t\t\"nome\": \"Tesouro Selic 2025\"\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"valorInvestido\": 467.1470,\r\n\t\t\t\"valorTotal\": 502.787,\r\n\t\t\t\"vencimento\": \"2020-02-01T00:00:00\",\r\n\t\t\t\"dataDeCompra\": \"2010-02-10T00:00:00\",\r\n\t\t\t\"iof\": 0,\r\n\t\t\t\"indice\": \"IPCA\",\r\n\t\t\t\"tipo\": \"TD\",\r\n\t\t\t\"nome\": \"Tesouro IPCA 2035\"\r\n\t\t}\r\n\t]\r\n}";

        [TestInitialize]
        public void Initialize()
        {
            _httpFactory = new Mock<IHttpClientFactory>();
            _logger = new Mock<ILogger>();
            _configurationSection = new Mock<IConfigurationSection>();
        }

        [TestMethod]
        public async Task ShouldReturnTesouDiretoWhenB3ServiceIsUp()
        {
            _configurationSection.Setup(x => x.GetSection(It.IsAny<string>())).Returns(CreateFakeConfigurationSection());

            var client = new HttpClient(HttpMessageHandlerBuilder.Create(HttpStatusCode.OK, JsonSucess, HttpMethod.Get, "http://test.com.br/unitest"))
            {
                BaseAddress = new Uri("http://test.com.br/")
            };

            _httpFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var service = new B3TesouroDiretoService(_httpFactory.Object, _logger.Object, _configurationSection.Object);
            var result = await service.GetAllByAccountIdAsync(1234567899);

            Assert.IsTrue(result.Count == 2);
        }

        [TestMethod]
        public async Task ShouldNotReturnTesouDiretoWhenB3ServiceReturnUnsuccessfully()
        {
            _configurationSection.Setup(x => x.GetSection(It.IsAny<string>())).Returns(CreateFakeConfigurationSection());

            var client = new HttpClient(HttpMessageHandlerBuilder.Create(HttpStatusCode.InternalServerError, "", HttpMethod.Get, "http://test.com.br/unitest"))
            {
                BaseAddress = new Uri("http://test.com.br/")
            };

            _httpFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var service = new B3TesouroDiretoService(_httpFactory.Object, _logger.Object, _configurationSection.Object);
            var result = await service.GetAllByAccountIdAsync(1234567899);

            Assert.IsTrue(result.Count == 0);
        }

        private IConfigurationSection CreateFakeConfigurationSection()
        {
            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(x => x.Path).Returns("AppSettings:Mockyio:RequestUri");
            configurationSection.Setup(x => x.Key).Returns("RequestUri");
            configurationSection.Setup(x => x.Value).Returns("http://test.com.br/configurationSection");
            return configurationSection.Object;
        }
    }
}
