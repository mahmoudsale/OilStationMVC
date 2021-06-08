 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class SubledgerTypeRepository : IMainRepository<SubledgerType>
    {
        DataBase.DB _db;
        public SubledgerTypeRepository(DataBase.DB db)
        {
            _db = db;
        }
        public void Add(SubledgerType entity)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> ObjectExists(object Id)
        {
            return ObjectConverter.CBool(await FindByIdAsync(Id));
        }
        public Task<int> AddAsync(SubledgerType entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(object Id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteByIdAsync(object Id)
        {
            throw new NotImplementedException();
        }

        public SubledgerType FindById(object Id)
        {
            throw new NotImplementedException();
        }

        public async Task<SubledgerType> FindByIdAsync(object Id)
        {
          return await _db.SubledgerTypes.SingleOrDefaultAsync(A => A.Id == ObjectConverter.CInt(Id));
        }

        public List<SubledgerType> List()
        {
            throw new NotImplementedException();
        }

        public Task<List<SubledgerType>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<SubledgerType>> ListAsync(object JournalType, object SubledgerTypeId)
        {
            throw new NotImplementedException();
        }

        public List<SubledgerType> Search(object term)
        {
            throw new NotImplementedException();
        }

        public void Update(object Id, SubledgerType entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(object Id, SubledgerType entity)
        {
            throw new NotImplementedException();
        }
    }
}
