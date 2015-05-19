namespace WatDuangDee.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Datetime : DbMigration
    {
        public override void Up()
        {
            
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Videos");
            DropTable("dbo.QuestionAnswerDhammas");
            DropTable("dbo.News");
            DropTable("dbo.Lessons");
            DropTable("dbo.Histories");
            DropTable("dbo.Galleries");
            DropTable("dbo.Activities");
        }
    }
}
