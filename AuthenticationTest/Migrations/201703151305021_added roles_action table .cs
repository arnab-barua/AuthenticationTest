namespace AuthenticationTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedroles_actiontable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_userstbl_roles", "tbl_users_U_Id", "dbo.tbl_users");
            DropForeignKey("dbo.tbl_userstbl_roles", "tbl_roles_Role_Id", "dbo.tbl_roles");
            DropIndex("dbo.tbl_userstbl_roles", new[] { "tbl_users_U_Id" });
            DropIndex("dbo.tbl_userstbl_roles", new[] { "tbl_roles_Role_Id" });
            CreateTable(
                "dbo.tbl_role_actions",
                c => new
                    {
                        Action_Id = c.Int(nullable: false, identity: true),
                        Action_Name = c.String(),
                    })
                .PrimaryKey(t => t.Action_Id);
            
            CreateTable(
                "dbo.tbl_role_actionstbl_roles",
                c => new
                    {
                        tbl_role_actions_Action_Id = c.Int(nullable: false),
                        tbl_roles_Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.tbl_role_actions_Action_Id, t.tbl_roles_Role_Id })
                .ForeignKey("dbo.tbl_role_actions", t => t.tbl_role_actions_Action_Id, cascadeDelete: true)
                .ForeignKey("dbo.tbl_roles", t => t.tbl_roles_Role_Id, cascadeDelete: true)
                .Index(t => t.tbl_role_actions_Action_Id)
                .Index(t => t.tbl_roles_Role_Id);
            
            AddColumn("dbo.tbl_users", "Role_id", c => c.Int());
            CreateIndex("dbo.tbl_users", "Role_id");
            AddForeignKey("dbo.tbl_users", "Role_id", "dbo.tbl_roles", "Role_Id");
            DropTable("dbo.tbl_userstbl_roles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tbl_userstbl_roles",
                c => new
                    {
                        tbl_users_U_Id = c.Int(nullable: false),
                        tbl_roles_Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.tbl_users_U_Id, t.tbl_roles_Role_Id });
            
            DropForeignKey("dbo.tbl_users", "Role_id", "dbo.tbl_roles");
            DropForeignKey("dbo.tbl_role_actionstbl_roles", "tbl_roles_Role_Id", "dbo.tbl_roles");
            DropForeignKey("dbo.tbl_role_actionstbl_roles", "tbl_role_actions_Action_Id", "dbo.tbl_role_actions");
            DropIndex("dbo.tbl_role_actionstbl_roles", new[] { "tbl_roles_Role_Id" });
            DropIndex("dbo.tbl_role_actionstbl_roles", new[] { "tbl_role_actions_Action_Id" });
            DropIndex("dbo.tbl_users", new[] { "Role_id" });
            DropColumn("dbo.tbl_users", "Role_id");
            DropTable("dbo.tbl_role_actionstbl_roles");
            DropTable("dbo.tbl_role_actions");
            CreateIndex("dbo.tbl_userstbl_roles", "tbl_roles_Role_Id");
            CreateIndex("dbo.tbl_userstbl_roles", "tbl_users_U_Id");
            AddForeignKey("dbo.tbl_userstbl_roles", "tbl_roles_Role_Id", "dbo.tbl_roles", "Role_Id", cascadeDelete: true);
            AddForeignKey("dbo.tbl_userstbl_roles", "tbl_users_U_Id", "dbo.tbl_users", "U_Id", cascadeDelete: true);
        }
    }
}
