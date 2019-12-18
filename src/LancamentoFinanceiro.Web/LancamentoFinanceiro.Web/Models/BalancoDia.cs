using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LancamentoFinanceiro.Web.Models
{
    public class BalancoDia
    {
        public DateTime DatadoBalanco { get; set; }

        public decimal ValorTotalCredito { get; set; }

        public decimal ValorTotalDebito { get; set; }

        public decimal ValorSaldo { get; set; }

        public bool Totalizacao { get; set; }
    }
}
