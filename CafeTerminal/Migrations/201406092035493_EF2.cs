namespace CafeTerminal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EF2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vare", "Farge", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vare", "Farge");
        }
    }
}
