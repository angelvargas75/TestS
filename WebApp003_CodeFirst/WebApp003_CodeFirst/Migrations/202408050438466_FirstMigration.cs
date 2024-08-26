namespace WebApp003_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservas",
                c => new
                    {
                        ReservaId = c.Int(nullable: false, identity: true),
                        SalaId = c.Int(nullable: false),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        Usuario = c.String(),
                    })
                .PrimaryKey(t => t.ReservaId)
                .ForeignKey("dbo.Salas", t => t.SalaId, cascadeDelete: true)
                .Index(t => t.SalaId);
            
            CreateTable(
                "dbo.Salas",
                c => new
                    {
                        SalaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Capacidad = c.Int(nullable: false),
                        Recursos = c.String(),
                        Comentarios = c.String(),
                    })
                .PrimaryKey(t => t.SalaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "SalaId", "dbo.Salas");
            DropIndex("dbo.Reservas", new[] { "SalaId" });
            DropTable("dbo.Salas");
            DropTable("dbo.Reservas");
        }
    }
}
