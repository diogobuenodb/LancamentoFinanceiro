using System;
using System.Collections.Generic;
using System.Text;

namespace LancamentoFinanceiro.Business.Models
{
    public class Tipo
    {
        public int TipoId { get; set; }
        public string Nome { get; set; }

        //[TODO] Salvar dados no Banco
        public List<Tipo> ListaTipos()
        {
            return new List<Tipo>
                    {
                        new Tipo { TipoId = 1, Nome = "Débito"},
                        new Tipo { TipoId = 2, Nome = "Crédito"},

                    };
        }
    }
}
