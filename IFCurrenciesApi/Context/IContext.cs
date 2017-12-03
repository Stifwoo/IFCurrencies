using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using IFCurrenciesApi.Models;

namespace IFCurrenciesApi.Context
{
    public interface IContext
    {
        IDbSet<Bank> Banks { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
    }
}
