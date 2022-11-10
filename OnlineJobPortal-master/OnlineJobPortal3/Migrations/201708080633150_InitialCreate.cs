namespace OnlineJobPortal3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
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
                        CompanyName = c.String(),
                        HeadOfficeCity = c.String(),
                        HeadOfficeContactNo = c.String(),
                        NoOfBranch = c.Int(),
                        CompanyMail = c.String(),
                        CompanyType = c.String(),
                        CompanyWebsite = c.String(),
                        Title = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ContactNo = c.String(),
                        BirthDate = c.DateTime(),
                        Gender = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
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
                "dbo.JobPosts",
                c => new
                    {
                        JobPostID = c.Int(nullable: false, identity: true),
                        JobName = c.String(nullable: false),
                        JobDescription = c.String(nullable: false),
                        Location = c.String(nullable: false),
                        WorkExperience = c.Int(nullable: false),
                        MinimumSalary = c.Int(nullable: false),
                        ApplcationUserID = c.String(),
                        Company_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.JobPostID)
                .ForeignKey("dbo.AspNetUsers", t => t.Company_Id)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.JobApplications",
                c => new
                    {
                        JobApplicationID = c.Int(nullable: false, identity: true),
                        Qualification = c.String(nullable: false),
                        CourseStudied = c.String(nullable: false),
                        YearOfGraduation = c.Int(nullable: false),
                        Experience = c.Int(nullable: false),
                        Designation = c.String(),
                        JobSeekerId = c.String(maxLength: 128),
                        JobPostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JobApplicationID)
                .ForeignKey("dbo.JobPosts", t => t.JobPostId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.JobSeekerId)
                .Index(t => t.JobSeekerId)
                .Index(t => t.JobPostId);
            
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        msg = c.String(nullable: false),
                        SenderID = c.String(maxLength: 128),
                        RecipientID = c.String(maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.MessageID)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderID)
                .ForeignKey("dbo.AspNetUsers", t => t.RecipientID)
                .Index(t => t.SenderID)
                .Index(t => t.RecipientID);
            
            CreateTable(
                "dbo.NewsFeeds",
                c => new
                    {
                        NewsFeedID = c.Int(nullable: false, identity: true),
                        NewsTitle = c.String(nullable: false),
                        News = c.String(nullable: false),
                        DateOfPublication = c.DateTime(nullable: false),
                        NewsToWhom = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NewsFeedID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Messages", "RecipientID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "SenderID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobPosts", "Company_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobApplications", "JobSeekerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobApplications", "JobPostId", "dbo.JobPosts");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Messages", new[] { "RecipientID" });
            DropIndex("dbo.Messages", new[] { "SenderID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.JobApplications", new[] { "JobPostId" });
            DropIndex("dbo.JobApplications", new[] { "JobSeekerId" });
            DropIndex("dbo.JobPosts", new[] { "Company_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.NewsFeeds");
            DropTable("dbo.Messages");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.JobApplications");
            DropTable("dbo.JobPosts");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
        }
    }
}
