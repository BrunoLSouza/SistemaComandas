using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCS.SistemaComanda.Dominio
{
    [Table("ItemComanda")]
    public class ItemComanda
    {
        [Key]
        public Guid IdItemComanda { get; set; }
               
        public int IdComanda { get; set; }

        [ForeignKey("Produto")]
        public int IdProduto { get; set; }
        public virtual Produto Produto { get; set; }

        public int Quantidade { get; set; }

    }
}
