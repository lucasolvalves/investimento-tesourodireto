using System;

namespace Investimento.TesouroDireto.Domain.Entities
{
    public class TesouroDireto
    {
        public TesouroDireto(double valorInvestido, double valorTotal, DateTime dataDeVencimento, DateTime dataDeCompra, double iOF, string indice, string tipo, string nome)
        {
            Nome = nome;
            ValorInvestido = valorInvestido;
            ValorTotal = valorTotal;
            DataDeVencimento = dataDeVencimento;
            DataDeCompra = dataDeCompra;
            IOF = iOF;
            Indice = indice;
            Tipo = tipo;

            CalcularIR();
            CalcularResgate();
        }

        public string Nome { get; private set; }
        public double ValorInvestido { get; private set; }
        public double ValorTotal { get; private set; }
        public DateTime DataDeVencimento { get; private set; }
        public DateTime DataDeCompra { get; private set; }
        public double IOF { get; private set; }
        public double IR { get; private set; }
        public double ValorResgate { get; private set; }
        public string Indice { get; private set; }
        public string Tipo { get; private set; }
        private double TaxaRentabilidade => 10;

        private double CalcularIR()
        {
            var rentabilidade = ValorTotal - ValorInvestido;

            if (rentabilidade <= 0)
                return IR = 0;

            return IR = (rentabilidade * (TaxaRentabilidade / 100));
        }

        private void CalcularResgate()
        {
            var mesesInicioVencimento = Math.Truncate(DataDeVencimento.Subtract(DataDeCompra).Days / (365.25 / 12));
            var mesesInicioAtual = Math.Truncate(DateTime.Now.Subtract(DataDeCompra).Days / (365.25 / 12));

            if ((mesesInicioVencimento - mesesInicioAtual) <= 3)
            {
                //Investimento com até 3 meses para vencer: Perde 6% do valor investido
                var desconto = (ValorTotal * ((double)6 / 100));
                ValorResgate = ValorTotal - desconto;
            }
            else if (mesesInicioAtual > (mesesInicioVencimento / 2))
            {
                //Investimento com mais da metade do tempo em custódia: Perde 15% do valor investido
                var desconto = (ValorTotal * ((double)15 / 100));
                ValorResgate = ValorTotal - desconto;
            }
            else
            {
                //Outros: Perde 30% do valor investido
                var desconto = (ValorTotal * ((double)30 / 100));
                ValorResgate = ValorTotal - desconto;
            }
        }
    }
}
