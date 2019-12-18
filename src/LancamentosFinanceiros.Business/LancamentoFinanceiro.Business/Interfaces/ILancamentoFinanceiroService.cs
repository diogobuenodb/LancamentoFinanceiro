using LancamentoFinanceiro.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks; 

namespace LancamentoFinanceiro.Business.Interfaces
{
    public interface ILancamentoFinanceiroService : IDisposable
    {
        Task Adicionar(Lancamento lancamentoFinanceiro);
        Task Atualizar(Lancamento lancamentoFinanceiro);
        Task Remover(Guid id);
        Task<Lancamento> ObterPorId(Guid id);
        Task<List<Lancamento>> ObterTodos();
        Task<IEnumerable<Lancamento>> Buscar(Expression<Func<Lancamento, bool>> predicate);

    }
}
