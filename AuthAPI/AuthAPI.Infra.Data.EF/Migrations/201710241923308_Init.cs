namespace AuthAPI.Infra.Data.EF.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Telefones",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                        Ddd = c.String(nullable: false, maxLength: 3),
                        Numero = c.String(nullable: false, maxLength: 9),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId)
                .Index(t => new { t.UsuarioId, t.Ddd, t.Numero }, unique: true, name: "unq_Telefone");
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 255),
                        Senha = c.String(nullable: false, maxLength: 8000),
                        DataCriacao = c.DateTime(nullable: false),
                        DataUpdate = c.DateTime(),
                        DataUltimoLogin = c.DateTime(),
                        Token = c.String(maxLength: 8000),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true, name: "unq_Usuarios_Email");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Telefones", "UsuarioId", "dbo.Usuarios");
            DropIndex("dbo.Usuarios", "unq_Usuarios_Email");
            DropIndex("dbo.Telefones", "unq_Telefone");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Telefones");
        }
    }
}
