namespace TCS.SistemaComanda.Dados.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TCS.SistemaComanda.Dados.Seeds;

    internal sealed class Configuration : DbMigrationsConfiguration<TCS.SistemaComanda.Dados.Contexto.CtxSistemaComanda>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TCS.SistemaComanda.Dados.Contexto.CtxSistemaComanda context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            ProdutoSeed.Seed(context);
            ComandaSeed.Seed(context);

        }
    }
}
