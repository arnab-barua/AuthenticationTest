namespace AuthenticationTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class signatureintbl_users : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_users", "signature", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_users", "signature");
        }
    }
}
