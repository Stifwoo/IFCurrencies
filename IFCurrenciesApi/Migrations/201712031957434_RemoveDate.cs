namespace IFCurrenciesApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Banks", "UpdateDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Banks", "UpdateDate", c => c.DateTime(nullable: false));
        }
    }
}
