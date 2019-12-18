using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LancamentoFinanceiro.Business.Interfaces;
using LancamentosFinanceiros.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LancamentoFinanceiro.Business.Models;


namespace LancamentosFinanceiros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LancamentosFinanceirosController : ControllerBase
    {
        private readonly ILancamentoFinanceiroService _lancamentoFinanceiroService;
        private readonly IMapper _mapper;

        public LancamentosFinanceirosController(ILancamentoFinanceiroService lancamentoFinanceiroService,
                                              IMapper mapper)
        {           
            _lancamentoFinanceiroService = lancamentoFinanceiroService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<LancamentoFinanceiroViewModel>> ObterTodos()
        {
            var lancamentosViewModeList = new List<LancamentoFinanceiroViewModel>();
            var lancamentosViewMode = new LancamentoFinanceiroViewModel();
            var lancamentos = await _lancamentoFinanceiroService.ObterTodos();

            foreach (var lancamento in lancamentos)
            {
                lancamentosViewMode = new LancamentoFinanceiroViewModel();

                lancamentosViewMode.Id = lancamento.Id;
                lancamentosViewMode.Valor = lancamento.Valor;
                lancamentosViewMode.DataLancamento = lancamento.DataLancamento;
                lancamentosViewMode.Status = lancamento.Status;
                lancamentosViewMode.Tipo = lancamento.Tipo;

                lancamentosViewModeList.Add(lancamentosViewMode);
            }

            // return _mapper.Map<IEnumerable<LancamentoFinanceiroViewModel>>(await _lancamentoFinanceiroService.ObterTodos());
            return lancamentosViewModeList.AsEnumerable();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<LancamentoFinanceiroViewModel>> ObterPorId(Guid id)
        {
            var produtoViewModel = await _lancamentoFinanceiroService.ObterPorId(id);

            if (produtoViewModel == null) return NotFound();

            return Ok(produtoViewModel);
        }

        [HttpPost]
        public async Task<ActionResult<LancamentoFinanceiroViewModel>> Adicionar(LancamentoFinanceiroViewModel lancamentoFinanceiroViewModel)
        {
            if (!ModelState.IsValid)
                return NotFound();

            var Lancamento = new Lancamento()
            {               
                DataLancamento = DateTime.Now,
                Valor = lancamentoFinanceiroViewModel.Valor,
                Tipo = lancamentoFinanceiroViewModel.Tipo,
                Status = lancamentoFinanceiroViewModel.Status
            };

            // await _lancamentoFinanceiroService.Adicionar(_mapper.Map<Lancamento>(lancamentoFinanceiroViewModel));

            await _lancamentoFinanceiroService.Adicionar(Lancamento);

            return Ok(Lancamento);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, LancamentoFinanceiroViewModel lancamentoFinanceiroViewModel)
        {

            var lancamentoFinanceiroAtualizacao = await _lancamentoFinanceiroService.ObterPorId(lancamentoFinanceiroViewModel.Id);

            if (!ModelState.IsValid) return NotFound();


            //⦁	Caso o lançamento já tenha sido conciliado não deve permitir edição nem deleção do registro;            
            if (lancamentoFinanceiroAtualizacao.Status == 2)
                return BadRequest();

            lancamentoFinanceiroAtualizacao.Valor = lancamentoFinanceiroViewModel.Valor;
            lancamentoFinanceiroAtualizacao.Tipo = lancamentoFinanceiroViewModel.Tipo;
            lancamentoFinanceiroAtualizacao.Status = lancamentoFinanceiroViewModel.Status;


            await _lancamentoFinanceiroService.Atualizar(_mapper.Map<Lancamento>(lancamentoFinanceiroAtualizacao));

            return Ok(lancamentoFinanceiroViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<LancamentoFinanceiroViewModel>> Excluir(Guid id)
        {
            //var lancamentoFinanceiroExcluir = await _lancamentoFinanceiroService.ObterPorId(id);

            //if (lancamentoFinanceiroExcluir == null) return NotFound();

            //⦁	Caso o lançamento já tenha sido conciliado não deve permitir edição nem deleção do registro;            
            //if (lancamentoFinanceiroExcluir.Status == 2)
            // return BadRequest(); 

            await _lancamentoFinanceiroService.Remover(id);

            return Ok();
        }

        [HttpGet("ObterPorData")]
        public async Task<ActionResult<BalancoDia>> ObterPorData(string tipoRelatorio, string dateTime)
        {
            // dateTime = Convert.ToDateTime(dateTime).ToString("dd/MM/yyy");
            var listBalancoDia = new List<BalancoDia>();
            var balancoDia = new BalancoDia();

            if (tipoRelatorio.ToUpper() == "D")
            {
                var produtoViewModel = await _lancamentoFinanceiroService.Buscar(p => p.DataLancamento.ToString("dd-MM-yyyy") == dateTime);

                ////Débito
                //var debito = produtoViewModel.Where(x => x.Tipo == 1).Select(x => x.Valor).Sum();
                //balancoDia.ValorTotalDebito = debito;

                ////Crédito
                //var credito = produtoViewModel.Where(x => x.Tipo == 2).Select(x => x.Valor).Sum();
                //balancoDia.ValorTotalCredito = credito;

                ////Valor saldo
                //balancoDia.ValorSaldo = balancoDia.ValorTotalCredito - balancoDia.ValorTotalDebito;

                foreach (var item in produtoViewModel)
                {
                    balancoDia = new BalancoDia();

                    //Débito
                    if (item.Tipo == 1)
                    {
                        balancoDia.ValorTotalDebito = item.Valor;

                        //Valor saldo
                        balancoDia.ValorSaldo -= item.Valor;

                    }
                    //Crédito
                    else if (item.Tipo == 2)
                    {
                        balancoDia.ValorTotalCredito = item.Valor;

                        //Valor saldo
                        balancoDia.ValorSaldo += item.Valor;
                    }

                    // Data do Balanço
                    balancoDia.DatadoBalanco = item.DataLancamento; 

                    //Adiciona o item na lista
                    listBalancoDia.Add(balancoDia);
                }
               
                return Ok(listBalancoDia);
            }
            else if (tipoRelatorio.ToUpper() == "M")
            {

                var produtoViewModel = await _lancamentoFinanceiroService.Buscar(p => p.DataLancamento.ToString("MM-yyyy") == dateTime);

                produtoViewModel = produtoViewModel.ToList().OrderBy(x => x.DataLancamento).ToList();

                // Datas
                var filtraDatas = produtoViewModel.Select(x => x.DataLancamento.ToString("dd/MM/yyyy")).Distinct().ToList();


                foreach (var fatura in filtraDatas)
                {
                    var Datafatura = fatura;

                    foreach (var item in produtoViewModel)
                    {
                        // Obtem a data da Fatura
                        var dataItem = item.DataLancamento.ToString("dd/MM/yyyy");

                        // Verifica se a data é a mesma do laço
                        if (Datafatura == dataItem)
                        {

                            // Data do Balanço
                            balancoDia.DatadoBalanco = item.DataLancamento;

                            //Débito
                            if (item.Tipo == 1)
                            {
                                balancoDia.ValorTotalDebito += item.Valor;

                                //Valor saldo
                                balancoDia.ValorSaldo -= item.Valor;

                            }
                            //Crédito
                            else if (item.Tipo == 2)
                            {
                                balancoDia.ValorTotalCredito += item.Valor;

                                //Valor saldo
                                balancoDia.ValorSaldo += item.Valor;
                            }                           
                        }
                    } 

                    //Adiciona os Itens da Lista
                    listBalancoDia.Add(balancoDia);

                    balancoDia = new BalancoDia();
                }

                return Ok(listBalancoDia);
            }

            return NotFound();
        }

        [HttpGet("ObterStatus")]
        public IEnumerable<Status> ObterStatus()
        {
            var status = new Status();
            return status.ListaStatus();
        }

        [HttpGet("ObterTipo")]
        public IEnumerable<Tipo> ObterTipo()
        {
            var tipo = new Tipo();
            return tipo.ListaTipos();
        }
    }
}