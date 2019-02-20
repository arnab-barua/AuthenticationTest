namespace AuthenticationTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedusertable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_users",
                c => new
                    {
                        U_Id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        password = c.String(),
                        role = c.String(),
                    })
                .PrimaryKey(t => t.U_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tbl_users");
        }
    }
}
