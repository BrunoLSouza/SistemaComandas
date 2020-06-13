using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCS.SistemaComanda.Dominio
{
    [Table("AnotacaoNotaFiscal")]
    public class AnotacaoNotaFiscal
    {
        [Key]
        public Guid IdAnotacaoNotaFiscal { get; set; }

        public string Descricao { get; set; }

        public Guid IdNotaFiscal { get; set; }
    }
}
