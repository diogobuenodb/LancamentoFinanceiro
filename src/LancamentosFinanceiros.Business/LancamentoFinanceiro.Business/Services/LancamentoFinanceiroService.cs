using LancamentoFinanceiro.Business.Interfaces;
using LancamentoFinanceiro.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LancamentoFinanceiro.Business.Services
{
    public class LancamentoFinanceiroService :  ILancamentoFinanceiroService
    {
        private readonly ILancamentoFinanceiroRepository _lancamentoFinanceiroRepository;

        public Task<IEnumerable<Lancamento>> Buscar(Expression<Func<Lancamento, bool>> predicate)
        {
            return _lancamentoFinanceiroRepository.Buscar(predicate);
        }


        public LancamentoFinanceiroService(ILancamentoFinanceiroRepository lancamentoFinanceiroRepository )
        {
            _lancamentoFinanceiroRepository = lancamentoFinanceiroRepository;
        }

        public async Task Adicionar(Lancamento lancamentoFinanceiro)
        {

            await _lancamentoFinanceiroRepository.Adicionar(lancamentoFinanceiro);
        }

        public async Task Atualizar(Lancamento lancamentoFinanceiro)
        {

            await _lancamentoFinanceiroRepository.Atualizar(lancamentoFinanceiro);
        }

        public async Task Remover(Guid id)
        {
            await _lancamentoFinanceiroRepository.Remover(id);
        }

        public void Dispose()
        {
            _lancamentoFinanceiroRepository?.Dispose();
        }


        public async Task<Lancamento> ObterPorId(Guid id)
        {
            return await _lancamentoFinanceiroRepository.ObterPorId(id);
        }

        public async Task<List<Lancamento>> ObterTodos()
        {
            return await _lancamentoFinanceiroRepository.ObterTodos();
        }


         
    }
}
