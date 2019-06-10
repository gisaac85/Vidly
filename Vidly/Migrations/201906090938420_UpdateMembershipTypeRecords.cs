namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMembershipTypeRecords : DbMigration
    {
        public override void Up()
        {
            Sql("Update MembershipTypes SET Name='Pay As You Go' WHERE Id=1 ");
            Sql("Update MembershipTypes SET Name='Monthly' WHERE Id=2 ");
            
        }

        public override void Down()
        {
        }
    }
}
