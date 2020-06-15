using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCS.SistemaComanda.Dominio
{
    [Table("ItemNotaFiscal")]
    public class ItemNotaFiscal
    {
        [Key]
        public Guid IdItemNotaFiscal { get; set; }
       
        public string Produto { get; set; }
        public double Valor { get; set; }

        public int Quantidade { get; set; }

        public double Total { 
            get
            {
                return Valor * Quantidade;
            }
        }

        public Guid IdNotaFiscal { get; set; }
    }
}
