namespace WebApp003_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSalaModel21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Salas", "Comentarios", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Salas", "Comentarios", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
