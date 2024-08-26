namespace WebApp003_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSalaModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Salas", "Nombre", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Salas", "Comentarios", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Salas", "Comentarios", c => c.String());
            AlterColumn("dbo.Salas", "Nombre", c => c.String());
        }
    }
}
