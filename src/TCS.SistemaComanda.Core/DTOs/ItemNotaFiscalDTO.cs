using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCS.SistemaComanda.Core
{
    public class ItemNotaFiscalDTO
    {
        public Guid IdItemNotaFiscal { get; set; }

        public string Produto { get; set; }
        public double Valor { get; set; }

        public int Quantidade { get; set; }

        public double Total
        {
            get
            {
                return Valor * Quantidade;
            }
        }

    }
}
