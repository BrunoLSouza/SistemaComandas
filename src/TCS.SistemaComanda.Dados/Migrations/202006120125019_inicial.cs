namespace TCS.SistemaComanda.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comanda",
                c => new
                    {
                        IdComanda = c.Int(nullable: false, identity: true),
                        Aberta = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdComanda);
            
            CreateTable(
                "dbo.ItemComanda",
                c => new
                    {
                        IdItemComanda = c.Guid(nullable: false),
                        IdComanda = c.Int(nullable: false),
                        IdProduto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdItemComanda)
                .ForeignKey("dbo.Comanda", t => t.IdComanda)
                .ForeignKey("dbo.Produto", t => t.IdProduto)
                .Index(t => t.IdComanda)
                .Index(t => t.IdProduto);
            
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        IdProduto = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 150, unicode: false),
                        Valor = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.IdProduto);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemComanda", "IdProduto", "dbo.Produto");
            DropForeignKey("dbo.ItemComanda", "IdComanda", "dbo.Comanda");
            DropIndex("dbo.ItemComanda", new[] { "IdProduto" });
            DropIndex("dbo.ItemComanda", new[] { "IdComanda" });
            DropTable("dbo.Produto");
            DropTable("dbo.ItemComanda");
            DropTable("dbo.Comanda");
        }
    }
}
