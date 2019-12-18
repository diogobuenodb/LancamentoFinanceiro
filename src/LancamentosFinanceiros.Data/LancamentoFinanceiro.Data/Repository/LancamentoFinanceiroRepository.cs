using LancamentoFinanceiro.Business.Interfaces;
using LancamentoFinanceiro.Business.Models;
using LancamentoFinanceiro.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace LancamentoFinanceiro.Data.Repository
{
    public class LancamentoFinanceiroRepository : Repository<Lancamento>, ILancamentoFinanceiroRepository
    {
        public LancamentoFinanceiroRepository(LancamentoFinanceiroDbContext context) : base(context) { }
    }
}
