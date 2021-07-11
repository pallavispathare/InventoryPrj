namespace DBMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedItemTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_mstItemMaster",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemName = c.String(nullable: false),
                        ItemDescription = c.String(nullable: false),
                        ItemPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tb_mstItemMaster");
        }
    }
}
