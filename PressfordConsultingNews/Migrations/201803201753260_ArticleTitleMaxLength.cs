namespace PressfordConsultingNews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleTitleMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "Title", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "Title", c => c.String(nullable: false));
        }
    }
}
