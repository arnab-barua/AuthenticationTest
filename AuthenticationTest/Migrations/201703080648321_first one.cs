namespace AuthenticationTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstone : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_dept",
                c => new
                    {
                        D_id = c.Int(nullable: false, identity: true),
                        DName = c.String(),
                    })
                .PrimaryKey(t => t.D_id);
            
            CreateTable(
                "dbo.tbl_emp",
                c => new
                    {
                        e_iD = c.Int(nullable: false, identity: true),
                        e_Name = c.String(),
                    })
                .PrimaryKey(t => t.e_iD);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tbl_emp");
            DropTable("dbo.tbl_dept");
        }
    }
}
