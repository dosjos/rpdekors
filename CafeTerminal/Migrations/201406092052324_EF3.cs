namespace CafeTerminal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EF3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vare", "Farge", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vare", "Farge", c => c.String());
        }
    }
}
