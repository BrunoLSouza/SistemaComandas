using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCS.SistemaComanda.Core
{
    public class NotaFiscalDTO
    {
        public Guid IdNotaFiscal { get; set; }
        public virtual List<ItemNotaFiscalDTO> Itens { get; set; }
        public List<AnotacaoNotaFiscalDTO> Anotacoes { get; set; }
    }
}
