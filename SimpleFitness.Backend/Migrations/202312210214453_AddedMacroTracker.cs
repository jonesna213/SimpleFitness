namespace SimpleFitness.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMacroTracker : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DailyMacroTrackers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Day = c.DateTime(nullable: false),
                        Protein = c.Int(nullable: false),
                        Fat = c.Int(nullable: false),
                        Carbs = c.Int(nullable: false),
                        TotalCalories = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DailyMacroTrackers", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.DailyMacroTrackers", new[] { "User_Id" });
            DropTable("dbo.DailyMacroTrackers");
        }
    }
}
