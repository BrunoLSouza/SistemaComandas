using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCS.SistemaComanda.Dominio
{
    [Table("Comanda")]
    public class Comanda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdComanda { get; set; }
        //public int Numero { get; set; }
        public bool Aberta { get; set; }

        public virtual List<ItemComanda> Itens { get; set; }
    }
}
