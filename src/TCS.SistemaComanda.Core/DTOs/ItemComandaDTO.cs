using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCS.SistemaComanda.Core.DTOs
{
    public class ItemComandaDTO
    {
        
        public Guid IdItemComanda { get; set; }
        public int IdComanda { get; set; }
        public ProdutoDTO Produto { get; set; }
        public int Quantidade { get; set; }
    }
}
