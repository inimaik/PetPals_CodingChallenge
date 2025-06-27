namespace Dao.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Participants", "PetId", c => c.Int());
            CreateIndex("dbo.Participants", "PetId");
            AddForeignKey("dbo.Participants", "PetId", "dbo.Pets", "PetId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Participants", "PetId", "dbo.Pets");
            DropIndex("dbo.Participants", new[] { "PetId" });
            DropColumn("dbo.Participants", "PetId");
        }
    }
}
