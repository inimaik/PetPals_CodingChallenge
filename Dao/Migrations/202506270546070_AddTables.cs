namespace Dao.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdoptionEvents",
                c => new
                    {
                        AdoptionEventId = c.Int(nullable: false, identity: true),
                        EventName = c.String(),
                        EventDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AdoptionEventId);
            
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        ParticipantId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AdoptionEventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParticipantId)
                .ForeignKey("dbo.AdoptionEvents", t => t.AdoptionEventId, cascadeDelete: true)
                .Index(t => t.AdoptionEventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Participants", "AdoptionEventId", "dbo.AdoptionEvents");
            DropIndex("dbo.Participants", new[] { "AdoptionEventId" });
            DropTable("dbo.Participants");
            DropTable("dbo.AdoptionEvents");
        }
    }
}
