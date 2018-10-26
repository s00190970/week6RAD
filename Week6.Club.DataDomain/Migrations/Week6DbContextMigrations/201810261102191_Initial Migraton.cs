namespace Week6.Club.DataDomain.Migrations.Week6DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigraton : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        MemberID = c.Int(nullable: false, identity: true),
                        AssociatedClub = c.Int(nullable: false),
                        StudentNumber = c.String(maxLength: 128),
                        approved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MemberID)
                .ForeignKey("dbo.Club", t => t.AssociatedClub, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentNumber)
                .Index(t => t.AssociatedClub)
                .Index(t => t.StudentNumber);
            
            CreateTable(
                "dbo.Club",
                c => new
                    {
                        ClubId = c.Int(nullable: false, identity: true),
                        ClubName = c.String(),
                        CreationDate = c.DateTime(nullable: false, storeType: "date"),
                        adminID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClubId);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        StudentNumber = c.String(nullable: false, maxLength: 128),
                        Firstname = c.String(),
                        Surname = c.String(),
                    })
                .PrimaryKey(t => t.StudentNumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Member", "StudentNumber", "dbo.Student");
            DropForeignKey("dbo.Member", "AssociatedClub", "dbo.Club");
            DropIndex("dbo.Member", new[] { "StudentNumber" });
            DropIndex("dbo.Member", new[] { "AssociatedClub" });
            DropTable("dbo.Student");
            DropTable("dbo.Club");
            DropTable("dbo.Member");
        }
    }
}
