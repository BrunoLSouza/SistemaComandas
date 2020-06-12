namespace TCS.SistemaComanda.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
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
                        Quantidade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdItemComanda)
                .ForeignKey("dbo.Produto", t => t.IdProduto)
                .ForeignKey("dbo.Comanda", t => t.IdComanda)
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
            DropForeignKey("dbo.ItemComanda", "IdComanda", "dbo.Comanda");
            DropForeignKey("dbo.ItemComanda", "IdProduto", "dbo.Produto");
            DropIndex("dbo.ItemComanda", new[] { "IdProduto" });
            DropIndex("dbo.ItemComanda", new[] { "IdComanda" });
            DropTable("dbo.Produto");
            DropTable("dbo.ItemComanda");
            DropTable("dbo.Comanda");
        }
    }
}
