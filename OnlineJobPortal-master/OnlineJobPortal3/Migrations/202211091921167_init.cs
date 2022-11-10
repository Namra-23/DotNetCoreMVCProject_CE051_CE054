namespace OnlineJobPortal3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "JobSeeker_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Messages", "Company_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Messages", "JobSeeker_Id");
            CreateIndex("dbo.Messages", "Company_Id");
            AddForeignKey("dbo.Messages", "JobSeeker_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Messages", "Company_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Company_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "JobSeeker_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "Company_Id" });
            DropIndex("dbo.Messages", new[] { "JobSeeker_Id" });
            DropColumn("dbo.Messages", "Company_Id");
            DropColumn("dbo.Messages", "JobSeeker_Id");
        }
    }
}
