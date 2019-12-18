using LancamentoFinanceiro.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LancamentoFinanceiro.Data.Context
{
    public class LancamentoFinanceiroDbContext : DbContext
    {
        public LancamentoFinanceiroDbContext(DbContextOptions<LancamentoFinanceiroDbContext> options) : base(options)
        {
            Database.EnsureCreated(); 
        }

        public DbSet<Lancamento> LancamentoFinanceiros { get; set; }

        public DbSet<BalancoDia> BalancosDia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LancamentoFinanceiroDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
