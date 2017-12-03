namespace IFCurrenciesApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        Street = c.String(),
                        Building = c.String(),
                        Location_Latitude = c.Double(nullable: false),
                        Location_Longitude = c.Double(nullable: false),
                        Bank_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Banks", t => t.Bank_Id)
                .Index(t => t.Bank_Id);
            
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        OldId = c.Int(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UpdateDate = c.DateTime(nullable: false),
                        Usd_BuyRate = c.Double(nullable: false),
                        Usd_SellRate = c.Double(nullable: false),
                        Eur_BuyRate = c.Double(nullable: false),
                        Eur_SellRate = c.Double(nullable: false),
                        Rub_BuyRate = c.Double(nullable: false),
                        Rub_SellRate = c.Double(nullable: false),
                        Bank_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Banks", t => t.Bank_Id)
                .Index(t => t.Bank_Id);
            
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
        
        public override void Down()
        {
            DropForeignKey("dbo.Currencies", "Bank_Id", "dbo.Banks");
            DropForeignKey("dbo.Addresses", "Bank_Id", "dbo.Banks");
            DropIndex("dbo.Currencies", new[] { "Bank_Id" });
            DropIndex("dbo.Addresses", new[] { "Bank_Id" });
            DropTable("dbo.CurrencyExchangeRates");
            DropTable("dbo.Currencies");
            DropTable("dbo.Banks");
            DropTable("dbo.Addresses");
        }
    }
}
