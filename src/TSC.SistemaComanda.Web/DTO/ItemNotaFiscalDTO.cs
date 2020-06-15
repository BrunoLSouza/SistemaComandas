using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC.SistemaComanda.Web.DTO
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