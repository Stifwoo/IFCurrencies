using System;
using System.Collections.Generic;
using IFCurrenciesApi.Data;
using IFCurrenciesApi.Models;

namespace IFCurrenciesApi.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Context.IFCurrenciesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Context.IFCurrenciesContext context)
        {
            var curr = new List<Currencies>()
            {
                new Currencies()
                {
                    UpdateDate = DateTime.Now.Date,
                    Usd = new Usd(){BuyRate = 27, SellRate = 27.5},
                    Eur = new Eur(){BuyRate = 31.5, SellRate = 32},
                    Rub = new Rub(){BuyRate = 0.45, SellRate = 0.55},
                }
            };

            /*var addresses = new List<Address>();

            for (var i = 0; i < BankLocations.PrivatBank.Length; i += 2)
            {
                var address = new Address()
                {
                    Location = new Location()
                    {
                        Latitude = BankLocations.PrivatBank[i],
                        Longitude = BankLocations.PrivatBank[i + 1]
                    }
                };

                addresses.Add(address);
            } 

            var pruvatBank = new Bank() {Name = "ПриватБанк", Addresses = addresses, Currencies = curr, OldId = 1};

            context.Banks.AddOrUpdate(pruvatBank);*/

            /*var addresses = new List<Address>();

            for (int i = 0; i < BankLocations.OschadBank.Length; i += 2)
            {
                var address = new Address()
                {
                    Location = new Location()
                    {
                        Latitude = BankLocations.OschadBank[i],
                        Longitude = BankLocations.OschadBank[i + 1]
                    }
                };

                addresses.Add(address);
            }
            var oschadBank = new Bank() { Name = "ОщадБанк", Addresses = addresses, Currencies = curr, OldId = 2};
            context.Banks.AddOrUpdate(oschadBank);*/

            /*var addresses = new List<Address>();

            for (int i = 0; i < BankLocations.RaifaizenBank.Length; i += 2)
            {
                var address = new Address()
                {
                    Location = new Location()
                    {
                        Latitude = BankLocations.RaifaizenBank[i],
                        Longitude = BankLocations.RaifaizenBank[i + 1]
                    }
                };

                addresses.Add(address);
            }
            var raifaizenBank = new Bank() { Name = "Райффайзен Банк", Addresses = addresses, Currencies = curr, OldId = 3 };
            context.Banks.AddOrUpdate(raifaizenBank);*/

            /*var addresses = new List<Address>();

            for (int i = 0; i < BankLocations.UkrsocBank.Length; i += 2)
            {
                var address = new Address()
                {
                    Location = new Location()
                    {
                        Latitude = BankLocations.UkrsocBank[i],
                        Longitude = BankLocations.UkrsocBank[i + 1]
                    }
                };

                addresses.Add(address);
            }
            var ukrsocBank = new Bank() { Name = "Укрсоцбанк", Addresses = addresses, Currencies = curr, OldId = 4 };
            context.Banks.AddOrUpdate(ukrsocBank);*/

            /*var addresses = new List<Address>();

            for (int i = 0; i < BankLocations.IdeaBank.Length; i += 2)
            {
                var address = new Address()
                {
                    Location = new Location()
                    {
                        Latitude = BankLocations.IdeaBank[i],
                        Longitude = BankLocations.IdeaBank[i + 1]
                    }
                };

                addresses.Add(address);
            }
            var ideaBank = new Bank() { Name = "Ідея Банк", Addresses = addresses, Currencies = curr, OldId = 5 };
            context.Banks.AddOrUpdate(ideaBank);*/

            /*var addresses = new List<Address>();

            for (int i = 0; i < BankLocations.PumbBank.Length; i += 2)
            {
                var address = new Address()
                {
                    Location = new Location()
                    {
                        Latitude = BankLocations.PumbBank[i],
                        Longitude = BankLocations.PumbBank[i + 1]
                    }
                };

                addresses.Add(address);
            }
            var pumbBank = new Bank() { Name = "ПУМБ", Addresses = addresses, Currencies = curr, OldId = 6};
            context.Banks.AddOrUpdate(pumbBank);*/

            var addresses = new List<Address>();

            for (int i = 0; i < BankLocations.AlfaBank.Length; i += 2)
            {
                var address = new Address()
                {
                    Location = new Location()
                    {
                        Latitude = BankLocations.AlfaBank[i],
                        Longitude = BankLocations.AlfaBank[i + 1]
                    }
                };

                addresses.Add(address);
            }
            var alfaBank = new Bank() { Name = "Альфа-Банк", Addresses = addresses, Currencies = curr, OldId = 7};
            context.Banks.AddOrUpdate(alfaBank);
        }
    }
}
