namespace PressfordConsultingNews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuthor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.AuthorId);

            Sql("INSERT INTO dbo.Authors(FirstName,LastName) VALUES ('Hari', 'Murugathas')");
            Sql("INSERT INTO dbo.Authors(FirstName,LastName) VALUES ('James', 'Rees')");
            Sql("INSERT INTO dbo.Authors(FirstName,LastName) VALUES ('Tony', 'Price')");
        }
        
        public override void Down()
        {
            DropTable("dbo.Authors");
        }
    }
}
