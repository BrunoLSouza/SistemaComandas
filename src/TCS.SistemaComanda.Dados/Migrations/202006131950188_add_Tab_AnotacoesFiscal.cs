namespace TCS.SistemaComanda.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_Tab_AnotacoesFiscal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnotacaoNotaFiscal",
                c => new
                    {
                        IdAnotacaoNotaFiscal = c.Guid(nullable: false),
                        Descricao = c.String(maxLength: 150, unicode: false),
                        IdNotaFiscal = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.IdAnotacaoNotaFiscal)
                .ForeignKey("dbo.NotaFiscal", t => t.IdNotaFiscal)
                .Index(t => t.IdNotaFiscal);
            
            DropColumn("dbo.ItemComanda", "Quantidade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemComanda", "Quantidade", c => c.Int(nullable: false));
            DropForeignKey("dbo.AnotacaoNotaFiscal", "IdNotaFiscal", "dbo.NotaFiscal");
            DropIndex("dbo.AnotacaoNotaFiscal", new[] { "IdNotaFiscal" });
            DropTable("dbo.AnotacaoNotaFiscal");
        }
    }
}
