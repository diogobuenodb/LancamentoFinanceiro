using LancamentoFinanceiro.Business.Interfaces;
using LancamentoFinanceiro.Business.Models;
using LancamentoFinanceiro.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LancamentoFinanceiro.Data.Repository
{
    public class BalancosDiaServiceRepository : Repository<BalancoDia>, IBalancosDiaServiceRepository
    {
        public BalancosDiaServiceRepository(LancamentoFinanceiroDbContext context) : base(context) { }

    }
}
