using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCS.SistemaComanda.Dominio;

namespace TCS.SistemaComanda.Dados.Contexto
{
    public class CtxSistemaComanda : DbContext
    {
        public CtxSistemaComanda()
            : base("SistemaComanda")
        {
        }


        public DbSet<Comanda> Comandas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ItemComanda> ItensComanda { get; set; }
        public DbSet<NotaFiscal> NotasFiscais { get; set; }
        public DbSet<ItemNotaFiscal> ItensNotaFiscal { get; set; }
        public DbSet<AnotacaoNotaFiscal> AnotacoesNotaFiscal { get; set; }
        


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //modelBuilder.Properties()
            //   .Where(p => p.Name == "Id" + p.ReflectedType.Name)
            //   .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                 .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(150));


            base.OnModelCreating(modelBuilder);

        }

    }
}
