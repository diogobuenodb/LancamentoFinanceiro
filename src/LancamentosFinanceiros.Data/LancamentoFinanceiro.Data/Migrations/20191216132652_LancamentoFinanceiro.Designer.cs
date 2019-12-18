﻿// <auto-generated />
using System;
using LancamentoFinanceiro.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LancamentoFinanceiro.Data.Migrations
{
    [DbContext(typeof(LancamentoFinanceiroDbContext))]
    [Migration("20191216132652_LancamentoFinanceiro")]
    partial class LancamentoFinanceiro
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LancamentoFinanceiro.Business.Models.LancamentoFinanceiro", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataLancamento");

                    b.Property<int>("Status");

                    b.Property<int>("Tipo");

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");

                    b.ToTable("LancamentoFinanceiros");
                });
#pragma warning restore 612, 618
        }
    }
}
