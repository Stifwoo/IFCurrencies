using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IFCurrenciesApi.Context;
using IFCurrenciesApi.Models;

namespace IFCurrenciesApi.Services
{
    public class BankService : EntityService<Bank>, IBankService
    {
        public BankService(IContext context)
            : base(context)
        {
        }

        public IEnumerable<Bank> GetAllBanksWithRelatedEntities()
        {
            return _dbSet.Include(e => e.Addresses)
                         .Include(e => e.Currencies);
        }

        public IEnumerable<Bank> GetAllBanksWithRelatedEntitiesByDate(DateTime date)
        {
            var records = _dbSet.Include(e => e.Currencies);

            var banks = new List<Bank>();

            foreach (var bank in records)
            {
                var curr = bank.Currencies.FirstOrDefault(c => c.UpdateDate.Date == date.Date);
                if (curr != null)
                {
                    banks.Add(bank);
                }
            }

            return banks;
        }

        public IEnumerable<Bank> GetAllBanksWithLatestCurrencyRates()
        {
            var records = _dbSet.Include(e => e.Addresses)
                                .Include(e => e.Currencies);

            var banks = new List<Bank>();

            foreach (var bank in records)
            {
                var curr = bank.Currencies.OrderByDescending(c => c.UpdateDate).ToList();

                if (curr.Count > 1)
                {
                    curr.RemoveRange(1, curr.Count - 1);
                    bank.Currencies = curr;
                }

                banks.Add(bank);
            }

            return banks;
        }

        public Bank GetBankWithRelatedEntities(int id)
        {
            return _dbSet.Include(e => e.Addresses)
                         .Include(e => e.Currencies)
                         .FirstOrDefault(x => x.Id == id);
        }
    }
}