using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC.SistemaComanda.Web.Models
{
    public class NotaFiscalViewModel
    {
        public Guid IdNotaFiscal { get; set; }
        public virtual List<ItemNotaFiscalViewModel> Itens { get; set; }
        public List<AnotacaoNotaFiscalViewModel> Anotacoes { get; set; }
        public double Total { get; set; }
}
}