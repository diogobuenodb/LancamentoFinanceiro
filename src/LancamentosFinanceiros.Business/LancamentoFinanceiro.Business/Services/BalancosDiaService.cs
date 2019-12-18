using LancamentoFinanceiro.Business.Interfaces;
using LancamentoFinanceiro.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LancamentoFinanceiro.Business.Services
{
    public class BalancosDiaService : IBalancosDiaService
    {
        private readonly IBalancosDiaServiceRepository _IBalancosDiaServiceRepository;

        public BalancosDiaService(IBalancosDiaServiceRepository iBalancosDiaServiceRepository)
        {
            _IBalancosDiaServiceRepository = iBalancosDiaServiceRepository;
        }

        public Task<IEnumerable<BalancoDia>> Buscar(Expression<Func<BalancoDia, bool>> predicate)
        {
           return _IBalancosDiaServiceRepository.Buscar(predicate);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


    }
}
