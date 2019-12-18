using System;
using System.Collections.Generic;
using System.Text;

namespace LancamentoFinanceiro.Business.Models
{
    public class Status
    {
        public int StatusId { get; set; }
        public string Nome { get; set; }

        //[TODO] Salvar dados no Banco
        public List<Status> ListaStatus()
        {
            return new List<Status>
                    {
                        new Status { StatusId = 1, Nome = "Não conciliado"},
                        new Status { StatusId = 2, Nome = "Conciliado"},

                    };
        }
    }
}
