using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
  public  interface IFinancialRepository<TEntity>
    {
 
        Task<List<TEntity>> ListAsync();

        Task<List<TEntity>> ListAsync(object JournalType, object SubledgerTypeId);


        
        Task<TEntity> FindByIdAsync(object Id, object JournalType);

 
        Task<int> AddAsync(TEntity entity);

 
        Task<int> UpdateAsync(object Id, TEntity entity);

 
        Task<int> DeleteByIdAsync(object Id, object JournalType);

        List<TEntity> Search(object term);

        Task<bool> ObjectExists(object Id, object JournalType);
    }
}
