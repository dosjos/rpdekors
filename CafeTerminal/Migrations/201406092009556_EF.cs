namespace CafeTerminal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EF : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logg",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoggTid = c.DateTime(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Salg",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VareId = c.Int(nullable: false),
                        SlagsTid = c.DateTime(nullable: false),
                        Pris = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Svinn",
                c => new
                    {
                        SvinnId = c.Int(nullable: false, identity: true),
                        Navn = c.String(),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Kommentar = c.String(),
                        DatoTidspunkt = c.DateTime(nullable: false),
                        RegistrertTidspunkt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SvinnId);
            
            CreateTable(
                "dbo.UserLogg",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Brukstid = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Navn = c.String(),
                        Rolle = c.String(),
                        Slettet = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vare",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pris = c.Int(nullable: false),
                        Navn = c.String(),
                        CurrentlyInUse = c.Boolean(nullable: false),
                        Rank = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vare");
            DropTable("dbo.Users");
            DropTable("dbo.UserLogg");
            DropTable("dbo.Svinn");
            DropTable("dbo.Settings");
            DropTable("dbo.Salg");
            DropTable("dbo.Logg");
        }
    }
}
