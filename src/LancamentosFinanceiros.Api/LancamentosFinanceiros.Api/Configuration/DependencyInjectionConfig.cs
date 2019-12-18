using LancamentoFinanceiro.Business.Interfaces;
using LancamentoFinanceiro.Business.Services;
using LancamentoFinanceiro.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LancamentoFinanceiro.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<Data.Context.LancamentoFinanceiroDbContext>();
 
            services.AddScoped<ILancamentoFinanceiroRepository, LancamentoFinanceiroRepository>();
            services.AddScoped<IBalancosDiaServiceRepository, BalancosDiaServiceRepository>();

            services.AddScoped<ILancamentoFinanceiroService, LancamentoFinanceiroService>();
            services.AddScoped<IBalancosDiaService, BalancosDiaService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            return services;
        }
    }
}
