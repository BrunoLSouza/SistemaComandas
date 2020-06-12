using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCS.SistemaComanda.Dados.Contexto;
using TCS.SistemaComanda.Dominio;

namespace TCS.SistemaComanda.Dados
{
    public class ProdutoSeed
    {
        public static void Seed (CtxSistemaComanda context)
        {
            if (!context.Produtos.Any())
            {

               var p1 = new Produto { Nome = "Cerveja", Valor = 5.00 };
               var p2 = new Produto { Nome = "Conhaque", Valor = 20.00 };
               var p3 = new Produto { Nome = "Suco", Valor = 50.00 };
               var p4 = new Produto { Nome = "Água", Valor = 70.00 };

                context.Produtos.AddOrUpdate(p1);
                context.Produtos.AddOrUpdate(p2);
                context.Produtos.AddOrUpdate(p3);
                context.Produtos.AddOrUpdate(p4);
                
                context.SaveChanges();
            }
        }
    }
}
