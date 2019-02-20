namespace AuthenticationTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedroles_actiontabletodaynow : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.tbl_role_actionstbl_roles", newName: "tbl_rolestbl_role_actions");
            DropPrimaryKey("dbo.tbl_rolestbl_role_actions");
            AddPrimaryKey("dbo.tbl_rolestbl_role_actions", new[] { "tbl_roles_Role_Id", "tbl_role_actions_Action_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tbl_rolestbl_role_actions");
            AddPrimaryKey("dbo.tbl_rolestbl_role_actions", new[] { "tbl_role_actions_Action_Id", "tbl_roles_Role_Id" });
            RenameTable(name: "dbo.tbl_rolestbl_role_actions", newName: "tbl_role_actionstbl_roles");
        }
    }
}
