namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "Price", c => c.Decimal(nullable: false, precision: 9, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Items", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
