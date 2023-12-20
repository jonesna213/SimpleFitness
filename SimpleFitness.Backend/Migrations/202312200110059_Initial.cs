namespace SimpleFitness.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Age = c.Int(nullable: false),
                        Gender = c.String(),
                        Height = c.String(),
                        Weight = c.Int(nullable: false),
                        DailyActivity = c.String(),
                        Goal = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        MealPlan_Id = c.String(maxLength: 128),
                        WorkoutPlan_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MealPlans", t => t.MealPlan_Id)
                .ForeignKey("dbo.WorkoutPlans", t => t.WorkoutPlan_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.MealPlan_Id)
                .Index(t => t.WorkoutPlan_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.MealPlans",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CalorieLimit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DayOfWeek = c.String(),
                        MealType = c.String(),
                        Description = c.String(),
                        TotalProtein = c.Int(nullable: false),
                        TotalFat = c.Int(nullable: false),
                        TotalCarbs = c.Int(nullable: false),
                        TotalCalories = c.Int(nullable: false),
                        MealPlan_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MealPlans", t => t.MealPlan_Id)
                .Index(t => t.MealPlan_Id);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Protein = c.Int(nullable: false),
                        Fat = c.Int(nullable: false),
                        Carbs = c.Int(nullable: false),
                        Calories = c.Int(nullable: false),
                        FoodDescription = c.String(),
                        Meal_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Meals", t => t.Meal_Id)
                .Index(t => t.Meal_Id);
            
            CreateTable(
                "dbo.WorkoutPlans",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DayOfWeek = c.String(),
                        WorkoutPlan_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkoutPlans", t => t.WorkoutPlan_Id)
                .Index(t => t.WorkoutPlan_Id);
            
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
                        Sets = c.Int(nullable: false),
                        Reps = c.String(),
                        AmountOfTime = c.String(),
                        Workout_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Workouts", t => t.Workout_Id)
                .Index(t => t.Workout_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "WorkoutPlan_Id", "dbo.WorkoutPlans");
            DropForeignKey("dbo.Workouts", "WorkoutPlan_Id", "dbo.WorkoutPlans");
            DropForeignKey("dbo.Exercises", "Workout_Id", "dbo.Workouts");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "MealPlan_Id", "dbo.MealPlans");
            DropForeignKey("dbo.Meals", "MealPlan_Id", "dbo.MealPlans");
            DropForeignKey("dbo.Foods", "Meal_Id", "dbo.Meals");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.Exercises", new[] { "Workout_Id" });
            DropIndex("dbo.Workouts", new[] { "WorkoutPlan_Id" });
            DropIndex("dbo.Foods", new[] { "Meal_Id" });
            DropIndex("dbo.Meals", new[] { "MealPlan_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "WorkoutPlan_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "MealPlan_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.Exercises");
            DropTable("dbo.Workouts");
            DropTable("dbo.WorkoutPlans");
            DropTable("dbo.Foods");
            DropTable("dbo.Meals");
            DropTable("dbo.MealPlans");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
