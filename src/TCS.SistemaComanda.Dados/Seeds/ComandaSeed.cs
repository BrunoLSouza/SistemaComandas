using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCS.SistemaComanda.Dados.Contexto;
using TCS.SistemaComanda.Dominio;

namespace TCS.SistemaComanda.Dados.Seeds
{
    public class ComandaSeed
    {
        public static void Seed(CtxSistemaComanda context)
        {
            if (!context.Comandas.Any())
            {

                var c1 = new Comanda { Aberta = false };
                var c2 = new Comanda { Aberta = false };
                var c3 = new Comanda { Aberta = false };
                var c4 = new Comanda { Aberta = false };
                var c5 = new Comanda { Aberta = false };
                var c6 = new Comanda { Aberta = false };
                var c7 = new Comanda { Aberta = false };
                var c8 = new Comanda { Aberta = false };
                var c9 = new Comanda { Aberta = false };
                var c10 = new Comanda {Aberta = false };

                context.Comandas.AddOrUpdate(c1);
                context.Comandas.AddOrUpdate(c2);
                context.Comandas.AddOrUpdate(c3);
                context.Comandas.AddOrUpdate(c3);
                context.Comandas.AddOrUpdate(c4);
                context.Comandas.AddOrUpdate(c5);
                context.Comandas.AddOrUpdate(c6);
                context.Comandas.AddOrUpdate(c7);
                context.Comandas.AddOrUpdate(c8);
                context.Comandas.AddOrUpdate(c9);
                context.Comandas.AddOrUpdate(c10);

                context.SaveChanges();
            }
        }
    }
}
