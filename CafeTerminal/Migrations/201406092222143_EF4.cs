namespace CafeTerminal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EF4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserLogg", "UsersId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserLogg", "UsersId");
            AddForeignKey("dbo.UserLogg", "UsersId", "dbo.Users", "Id", cascadeDelete: true);
            DropColumn("dbo.UserLogg", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserLogg", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserLogg", "UsersId", "dbo.Users");
            DropIndex("dbo.UserLogg", new[] { "UsersId" });
            DropColumn("dbo.UserLogg", "UsersId");
        }
    }
}
