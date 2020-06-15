using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCS.SistemaComanda.Core.DTOs
{
    public class InserirProdutoDTO
    {
        public int IdComanda { get; set; }
        public List<int> IdProdutos { get; set; }
    }
}
