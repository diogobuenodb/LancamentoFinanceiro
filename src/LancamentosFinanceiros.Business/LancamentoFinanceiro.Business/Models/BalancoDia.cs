using System;
using System.Collections.Generic;
using System.Text;

namespace LancamentoFinanceiro.Business.Models
{
    public class BalancoDia : Entity
    {
        public DateTime DatadoBalanco { get; set; }

        public decimal ValorTotalCredito { get; set; }

        public decimal ValorTotalDebito { get; set; }

        public decimal ValorSaldo { get; set; }

    }
}
