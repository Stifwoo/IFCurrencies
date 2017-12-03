using System;
using System.Collections.Generic;
using IFCurrenciesApi.Models;

namespace IFCurrenciesApi.Services
{
    public interface IBankService : IEntityService<Bank>
    {
        IEnumerable<Bank> GetAllBanksWithRelatedEntities();

        IEnumerable<Bank> GetAllBanksWithRelatedEntitiesByDate(DateTime date);

        IEnumerable<Bank> GetAllBanksWithLatestCurrencyRates();

        Bank GetBankWithRelatedEntities(int id);
    }
}
