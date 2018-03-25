namespace PressfordConsultingNews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArticle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                {
                    ArticleId = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false),                    
                    Content = c.String(nullable: false, maxLength: 5000),
                    CreateDate = c.DateTime(nullable: false, defaultValueSql: "GetDate()"),
                    UpdateDate = c.DateTime(nullable: false, defaultValueSql: "GetDate()"),
                    AuthorId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ArticleId)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: false)
                .Index(t => t.AuthorId);

            Sql("INSERT INTO dbo.Articles(Title,Content,AuthorId) " +
                "VALUES ('Lorem ipsum', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. In sit amet ante ultrices, lacinia quam scelerisque, posuere diam. Maecenas elementum sagittis laoreet. Praesent facilisis suscipit sapien, ut finibus urna fermentum nec. Suspendisse potenti. Pellentesque dolor augue, tempor quis malesuada in, venenatis et nunc. Donec auctor quam in risus varius, a condimentum ex fringilla. Donec vel augue ligula.', 1)");

            Sql("INSERT INTO dbo.Articles(Title,Content,AuthorId) " +
                "VALUES ('Integer gravida rhoncus', 'Vivamus aliquam, risus quis commodo varius, nulla risus semper sem, eu facilisis ex velit et velit. Quisque nisl tortor, molestie quis blandit in, tincidunt a metus. Integer ornare pellentesque velit quis ornare. Mauris convallis id ante vitae pellentesque. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Donec orci nisi, porta vitae sem sed, tempor tristique dolor. Proin cursus lectus sapien, a gravida risus ultricies sit amet. Integer gravida rhoncus eros sit amet varius. ', 1)");

            Sql("INSERT INTO dbo.Articles(Title,Content,AuthorId) " +
                "VALUES ('Phasellus luctus ligula', 'Nullam in felis sit amet purus mollis interdum. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed porttitor elit sem, nec bibendum lacus ultricies sed. Nulla eget sapien lectus. Curabitur vitae quam convallis, finibus quam sed, tincidunt purus. ', 2)");

            Sql("INSERT INTO dbo.Articles(Title,Content,AuthorId) " +
                "VALUES ('Pellentesque eget justo', 'Quisque a dui et nulla elementum porta a a metus. Donec vel leo sit amet enim lobortis commodo. Integer ante arcu, tempus ac ipsum sit amet, placerat aliquet mi. Ut viverra finibus ex eu feugiat. Vestibulum sapien sem, ornare vel urna congue, auctor luctus enim. Vivamus lacinia nisl tortor, sit amet mollis quam molestie vel. Vestibulum fermentum a dui interdum tempus. Sed eu faucibus felis, sit amet posuere mauris. Mauris nec nisl a dolor feugiat feugiat eu eget est.', 3)");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Articles", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Articles", new[] { "AuthorId" });
            DropTable("dbo.Articles");
        }
    }
}
