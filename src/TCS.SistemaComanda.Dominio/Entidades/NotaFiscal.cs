using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCS.SistemaComanda.Dominio
{
    [Table("NotaFiscal")]
    public class NotaFiscal
    {
        [Key]
        public Guid IdNotaFiscal { get; set; }

        [ForeignKey("IdNotaFiscal")]
        public virtual List<ItemNotaFiscal> Itens { get; set; }

        [ForeignKey("IdNotaFiscal")]
        public List<AnotacaoNotaFiscal> Anotacoes { get; set; }
    }
}
