namespace AuthenticationTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetbl_empforreporting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_emp", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_emp", "Address", c => c.String());
            AddColumn("dbo.tbl_emp", "Phone", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_emp", "Nid", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_emp", "Nid");
            DropColumn("dbo.tbl_emp", "Phone");
            DropColumn("dbo.tbl_emp", "Address");
            DropColumn("dbo.tbl_emp", "Age");
        }
    }
}
