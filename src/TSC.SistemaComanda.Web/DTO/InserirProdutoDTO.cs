using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC.SistemaComanda.Web.DTO
{
    public class InserirProdutoDTO
    {
        public int IdComanda { get; set; }
        public List<int> IdProdutos { get; set; }
    }
}