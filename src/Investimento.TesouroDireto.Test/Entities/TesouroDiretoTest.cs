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
            var tesouroDireto = new Domain.Entities.TesouroDireto(799.4720, 829.68, DateTime.Parse("2021-05-01T00:00:00"), DateTime.Parse("2015-03-01T00:00:00"), 0, "SELIC", "TD", "Tesouro Selic 2025");
            Assert.IsTrue(tesouroDireto.ValorResgate == 779.8992);
        }

        [TestMethod]
        public void ShouldCalculateResgateMoreThanHalfExpirationDateofTesouDiretoWhenDataIsValid()
        {
            var tesouroDireto = new Domain.Entities.TesouroDireto(799.4720, 829.68, DateTime.Parse("2026-03-01T00:00:00"), DateTime.Parse("2015-03-01T00:00:00"), 0, "SELIC", "TD", "Tesouro Selic 2025");
            Assert.IsTrue(tesouroDireto.ValorResgate == 705.228);
        }

        [TestMethod]
        public void ShouldCalculateResgateLessThanHalfExpirationDateofTesouDiretoWhenDataIsValid()
        {
            var tesouroDireto = new Domain.Entities.TesouroDireto(799.4720, 829.68, DateTime.Parse("2026-03-01T00:00:00"), DateTime.Parse("2024-03-01T00:00:00"), 0, "SELIC", "TD", "Tesouro Selic 2025");
            Assert.IsTrue(tesouroDireto.ValorResgate == 580.776);
        }
    }
}
