using LancamentoFinanceiro.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LancamentoFinanceiro.Business.Interfaces
{
    public interface IBalancosDiaServiceRepository : IRepository<BalancoDia>
    {
        Task<IEnumerable<BalancoDia>> Buscar(Expression<Func<BalancoDia, bool>> predicate);
    }
}
