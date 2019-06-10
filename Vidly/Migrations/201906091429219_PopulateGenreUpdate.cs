namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenreUpdate : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Genres (Name) VALUES ('Family') ");
            Sql("insert into Genres (Name) VALUES ('Horror') ");
            Sql("insert into Genres (Name) VALUES ('Action') ");
            Sql("insert into Genres (Name) VALUES ('Drama') ");
            Sql("insert into Genres (Name) VALUES ('Romance') ");

        }
        
        public override void Down()
        {
        }
    }
}
