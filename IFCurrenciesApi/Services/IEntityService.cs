using System.Collections.Generic;
using IFCurrenciesApi.Models;

namespace IFCurrenciesApi.Services
{
    public interface IEntityService<T>
        where T : EntityBase
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);              
    }
}