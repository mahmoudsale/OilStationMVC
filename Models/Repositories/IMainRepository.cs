using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public interface IMainRepository<TEntity>
    { 
        Task<List<TEntity>> ListAsync(); 

        Task<TEntity> FindByIdAsync(object Id);
         
        Task<bool> AddAsync(TEntity entity);
         
        Task<bool> UpdateAsync(object Id, TEntity entity);
         
        Task<bool> DeleteByIdAsync(object Id);

        List<TEntity> Search(object term);

        Task<bool> ObjectExists(object Id);
    }
}
