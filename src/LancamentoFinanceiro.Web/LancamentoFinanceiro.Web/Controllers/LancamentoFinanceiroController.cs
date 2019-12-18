using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LancamentoFinanceiro.Business.Models;
using LancamentoFinanceiro.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace LancamentoFinanceiro.Web.Controllers
{
    public class LancamentoFinanceiroController : Controller
    {
        string URI = "https://localhost:44318/api/LancamentosFinanceiros/";

        // GET: LancamentoFinanceiro
        public async Task<IActionResult> Index()
        {
            var lancamentos = new List<Lancamento>();

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var ProdutoJsonString = await response.Content.ReadAsStringAsync();
                        lancamentos = JsonConvert.DeserializeObject<Lancamento[]>(ProdutoJsonString).ToList();
                    }
                    else
                    {
                        TempData["Erro"] = "Erro";
                    }

                }
            }
          
            return View(lancamentos);
        }

        // GET: LancamentoFinanceiro/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var lancamentos = new Lancamento();

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(String.Format("{0}{1}", URI, id)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var ProdutoJsonString = await response.Content.ReadAsStringAsync();
                        lancamentos = JsonConvert.DeserializeObject<Lancamento>(ProdutoJsonString);
                    }
                    else
                    {
                        TempData["Erro"] = "Erro";
                    }
                }
            } 

            //Tipo 
            var tipo = await BuscaTipo();
            ViewBag.TipoId = new SelectList(tipo, "TipoId", "Nome", lancamentos.Tipo);

            //Status 
            var status = await BuscaStatus();
            ViewBag.StatusId = new SelectList(status, "StatusId", "Nome", lancamentos.Status);
         
            return View(lancamentos);
        }

        // GET: LancamentoFinanceiro/Create
        public async Task<ActionResult> Create()
        {
            //Tipo 
            var tipo = await BuscaTipo();
            ViewBag.TipoId = new SelectList(tipo, "TipoId", "Nome");

            //Status 
            var status = await BuscaStatus();
            ViewBag.StatusId = new SelectList(status, "StatusId", "Nome");

            return View();
        }

        // POST: LancamentoFinanceiro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lancamento lancamento)
        {
            try
            {
                var lancamentos = new Lancamento();

                using (var client = new HttpClient())
                {
                    var serializedProduto = JsonConvert.SerializeObject(lancamento);
                    var content = new StringContent(serializedProduto, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(URI, content);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LancamentoFinanceiro/Edit/5

        public async Task<IActionResult> Edit(Guid id)
        {
            var lancamentos = new Lancamento();

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(String.Format("{0}{1}", URI, id)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var ProdutoJsonString = await response.Content.ReadAsStringAsync();
                        lancamentos = JsonConvert.DeserializeObject<Lancamento>(ProdutoJsonString);
                    }
                    else
                    {
                        TempData["Erro"] = "Erro";
                    }
                }
            }

            //Tipo 
            var tipo = await BuscaTipo();
            ViewBag.TipoId = new SelectList(tipo, "TipoId", "Nome", lancamentos.Tipo);

            //Status 
            var status = await BuscaStatus();
            ViewBag.StatusId = new SelectList(status, "StatusId", "Nome", lancamentos.Status);

            return View(lancamentos);
        }

        // POST: LancamentoFinanceiro/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Lancamento lancamento)
        {
            try
            {
                var lancamentos = new Lancamento();
                using (var client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.PutAsJsonAsync(URI + id, lancamento);
                    
                    if (!responseMessage.IsSuccessStatusCode)
                        TempData["Erro"] = "Erro";
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LancamentoFinanceiro/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var lancamentos = new Lancamento();

            using (var client = new HttpClient())
            {

                using (var response = await client.GetAsync(String.Format("{0}{1}", URI, id)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var ProdutoJsonString = await response.Content.ReadAsStringAsync();
                        lancamentos = JsonConvert.DeserializeObject<Lancamento>(ProdutoJsonString);
                    }
                    else
                    {
                        TempData["Erro"] = "Erro";
                    }

                }
            }

            //Tipo 
            var tipo = await BuscaTipo();
            ViewBag.TipoId = new SelectList(tipo, "TipoId", "Nome", lancamentos.Tipo);

            //Status 
            var status = await BuscaStatus();
            ViewBag.StatusId = new SelectList(status, "StatusId", "Nome", lancamentos.Status);            
           
            return View(lancamentos);
        }

        // POST: LancamentoFinanceiro/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, IFormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(URI);
                    HttpResponseMessage responseMessage = await
                                    client.DeleteAsync(String.Format("{0}{1}", URI, id));

                    if (!responseMessage.IsSuccessStatusCode)
                        TempData["Erro"] = "Erro";
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
        private async Task<List<Status>> BuscaStatus()
        {
            var status = new List<Status>();
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI + "ObterStatus"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var ProdutoJsonString = await response.Content.ReadAsStringAsync();
                        status = JsonConvert.DeserializeObject<Status[]>(ProdutoJsonString).ToList();
                    }
                    else
                    {
                        TempData["Erro"] = "Erro";
                    }

                }
            }

            return status;
        }

        private async Task<List<Tipo>> BuscaTipo()
        {
            var tipo = new List<Tipo>();
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI + "ObterTipo"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var ProdutoJsonString = await response.Content.ReadAsStringAsync();
                        tipo = JsonConvert.DeserializeObject<Tipo[]>(ProdutoJsonString).ToList();
                    }
                    else
                    {
                        TempData["Erro"] = "Erro";
                    }

                }
            }

            return tipo;
        }
    }
}