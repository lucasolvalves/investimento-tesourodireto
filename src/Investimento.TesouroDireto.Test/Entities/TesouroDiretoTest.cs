using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Investimento.TesouroDireto.Test.Entities
{
    [TestClass]
    public class TesouroDiretoTest
    {
        [TestMethod]
        public void ShouldCalculateIROfTesouDiretoWhenDataIsValid()
        {
            var tesouroDireto = new Domain.Entities.TesouroDireto(799.4720, 829.68, DateTime.Parse("2025-03-01T00:00:00"), DateTime.Parse("2015-03-01T00:00:00"), 0, "SELIC", "TD", "Tesouro Selic 2025");

            Assert.IsTrue(tesouroDireto.IR == 3.0207999999999973);
        }

        [TestMethod]
        public void ShouldNotCalculateIROfTesouDiretoWhenDataIsInvalid()
        {
            var tesouroDireto = new Domain.Entities.TesouroDireto(0, 0, DateTime.Parse("2025-03-01T00:00:00"), DateTime.Parse("2015-03-01T00:00:00"), 0, "SELIC", "TD", "Tesouro Selic 2025");
            Assert.IsTrue(tesouroDireto.IR == 0);
        }

        [TestMethod]
        public void ShouldCalculateResgateLessThan3MonthsExpirationDateofTesouDiretoWhenDataIsValid()
        {
            var tesouroDireto = new Domain.Entities.TesouroDireto(799.4720, 829.68, DateTime.Now.AddMonths(3), DateTime.Now.AddMonths(-3), 0, "SELIC", "TD", "Tesouro Selic 2025");
            Assert.IsTrue(tesouroDireto.ValorResgate == 779.8992);
        }

        [TestMethod]
        public void ShouldCalculateResgateMoreThanHalfExpirationDateofTesouDiretoWhenDataIsValid()
        {
            var tesouroDireto = new Domain.Entities.TesouroDireto(799.4720, 829.68, DateTime.Now.AddMonths(5), DateTime.Now.AddMonths(-7), 0, "SELIC", "TD", "Tesouro Selic 2025");
            Assert.IsTrue(tesouroDireto.ValorResgate == 705.228);
        }

        [TestMethod]
        public void ShouldCalculateResgateLessThanHalfExpirationDateofTesouDiretoWhenDataIsValid()
        {
            var tesouroDireto = new Domain.Entities.TesouroDireto(799.4720, 829.68, DateTime.Now.AddMonths(12), DateTime.Now.AddMonths(-7), 0, "SELIC", "TD", "Tesouro Selic 2025");
            Assert.IsTrue(tesouroDireto.ValorResgate == 580.776);
        }
    }
}
