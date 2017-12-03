namespace IFCurrenciesApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.CurrencyExchangeRates");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CurrencyExchangeRates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Currency = c.String(),
                        BuyRate = c.Double(nullable: false),
                        SellRate = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
