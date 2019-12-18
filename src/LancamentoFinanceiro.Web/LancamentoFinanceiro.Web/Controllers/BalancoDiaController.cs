using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LancamentoFinanceiro.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LancamentoFinanceiro.Web.Controllers
{
    public class BalancoDiaController : Controller
    {
        string URI = "https://localhost:44318/api/LancamentosFinanceiros/ObterPorData/";

        // GET: LancamentoFinanceiro
        public async Task<IActionResult> Index(string data)
        {
            var balancoDia = new List<BalancoDia>();

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI + "?tipoRelatorio=D&dateTime=" + DateTime.Now.ToString("dd-MM-yyyy")))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var ProdutoJsonString = await response.Content.ReadAsStringAsync();
                        balancoDia = JsonConvert.DeserializeObject<BalancoDia[]>(ProdutoJsonString).ToList();
                    }
                    else
                    {
                        TempData["Erro"] = "Erro";
                    }
                }

                // ⦁	O balanço do dia deve contemplar todos os lançamentos de crédito e débito diário com o 
                // cálculo de saldo; (CRÉDITO - DÉBITO)

                var totalizaDia = new BalancoDia();

                //TotalCredito
                totalizaDia.ValorTotalCredito = balancoDia.Sum(x => x.ValorTotalCredito);

                //TotalDebito
                totalizaDia.ValorTotalDebito = balancoDia.Sum(x => x.ValorTotalDebito);

                //ValorSaldo
                totalizaDia.ValorSaldo = balancoDia.Sum(x => x.ValorSaldo);

                // Indica que é a totalização mensal
                totalizaDia.Totalizacao = true;

                balancoDia.Add(totalizaDia);
            }
           
            return View(balancoDia);
        } 
      
        // GET: LancamentoFinanceiro
        public async Task<IActionResult> RelatorioMensal()
        {
            var balancoDia = new List<BalancoDia>();

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI + "?tipoRelatorio=M&dateTime=" + DateTime.Now.ToString("MM-yyyy")))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var ProdutoJsonString = await response.Content.ReadAsStringAsync();
                        balancoDia = JsonConvert.DeserializeObject<BalancoDia[]>(ProdutoJsonString).ToList();
                    }
                    else
                    {
                        TempData["Erro"] = "Erro";
                    }
                }
            }

            //⦁	O relatório mensal de balanço deve demonstrar para cada dia do mês qual foi o total de
            //crédito e débito bem como o saldo em sua totalização mensal;

            var totalizacaoMensal = new BalancoDia();

            //TotalCredito
            totalizacaoMensal.ValorTotalCredito = balancoDia.Sum(x => x.ValorTotalCredito);

            //TotalDebito
            totalizacaoMensal.ValorTotalDebito = balancoDia.Sum(x => x.ValorTotalDebito);

            //ValorSaldo
            totalizacaoMensal.ValorSaldo = balancoDia.Sum(x => x.ValorSaldo);

            // Indica que é a totalização mensal
            totalizacaoMensal.Totalizacao = true;

            balancoDia.Add(totalizacaoMensal);

            // URI = URI + id;
            return View(balancoDia);
        }
    }
}