namespace TCS.SistemaComanda.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_tab_NF_ItemNF_2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemNotaFiscal",
                c => new
                    {
                        IdItemNotaFiscal = c.Guid(nullable: false),
                        Produto = c.String(maxLength: 150, unicode: false),
                        Valor = c.Double(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        IdNotaFiscal = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.IdItemNotaFiscal)
                .ForeignKey("dbo.NotaFiscal", t => t.IdNotaFiscal)
                .Index(t => t.IdNotaFiscal);
            
            CreateTable(
                "dbo.NotaFiscal",
                c => new
                    {
                        IdNotaFiscal = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.IdNotaFiscal);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemNotaFiscal", "IdNotaFiscal", "dbo.NotaFiscal");
            DropIndex("dbo.ItemNotaFiscal", new[] { "IdNotaFiscal" });
            DropTable("dbo.NotaFiscal");
            DropTable("dbo.ItemNotaFiscal");
        }
    }
}
