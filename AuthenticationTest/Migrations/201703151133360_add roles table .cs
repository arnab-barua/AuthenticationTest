namespace AuthenticationTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrolestable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_roles",
                c => new
                    {
                        Role_Id = c.Int(nullable: false, identity: true),
                        role_name = c.String(),
                    })
                .PrimaryKey(t => t.Role_Id);
            
            CreateTable(
                "dbo.tbl_userstbl_roles",
                c => new
                    {
                        tbl_users_U_Id = c.Int(nullable: false),
                        tbl_roles_Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.tbl_users_U_Id, t.tbl_roles_Role_Id })
                .ForeignKey("dbo.tbl_users", t => t.tbl_users_U_Id, cascadeDelete: true)
                .ForeignKey("dbo.tbl_roles", t => t.tbl_roles_Role_Id, cascadeDelete: true)
                .Index(t => t.tbl_users_U_Id)
                .Index(t => t.tbl_roles_Role_Id);
            
            DropColumn("dbo.tbl_users", "role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_users", "role", c => c.String());
            DropForeignKey("dbo.tbl_userstbl_roles", "tbl_roles_Role_Id", "dbo.tbl_roles");
            DropForeignKey("dbo.tbl_userstbl_roles", "tbl_users_U_Id", "dbo.tbl_users");
            DropIndex("dbo.tbl_userstbl_roles", new[] { "tbl_roles_Role_Id" });
            DropIndex("dbo.tbl_userstbl_roles", new[] { "tbl_users_U_Id" });
            DropTable("dbo.tbl_userstbl_roles");
            DropTable("dbo.tbl_roles");
        }
    }
}
