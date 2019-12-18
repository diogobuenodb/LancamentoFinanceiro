using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LancamentoFinanceiro.Business.Interfaces;
using LancamentoFinanceiro.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LancamentoFinanceiro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalancosDiaController : ControllerBase
    {
        private readonly IBalancosDiaService _IBalancosDiaService;

        public BalancosDiaController(IBalancosDiaService iBalancosDiaService)
        {
            _IBalancosDiaService = iBalancosDiaService;
        }

        [HttpGet]
        public async Task<ActionResult<BalancoDia>> ObterPorData(string tipoRelatorio, DateTime dateTime)
        {
            dateTime =Convert.ToDateTime("12 /16/2019 4:12:56 PM");
            var produtoViewModel = await _IBalancosDiaService.Buscar(p => p.DatadoBalanco == dateTime);

            if (produtoViewModel == null) return NotFound();

            return Ok(produtoViewModel);
        }
    }
}