 
using Newtonsoft.Json; 
using OilStationMVC.Models;
using OilStationMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OilStationDotNetCore.Models.Repositories
{
    public class CarApiRepository : IMainRepository<Car>
    {
        DataBase.DB _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public CarApiRepository(OilStationMVC.DataBase.DB db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        } 
        public async Task<int> AddAsync(Car entity)
        {
            using (var client = new HttpClient())
            {

                var Endpoint = "https://localhost:44393/api/car";
                var content = ApiHelper. CreateHttpContent(entity);
                var result = await client.PostAsync(Endpoint, content);
                string resultContent = await result.Content.ReadAsStringAsync();
            }
            return 1;
        }

        public async Task<int> UpdateAsync(object Id, Car entity)
        {
            using (var client = new HttpClient())
            {
                var UserToken = _session.GetString("UserToken");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + UserToken);
                //client.DefaultRequestHeaders.Add("x-version", "1.1");
                var Endpoint = "https://localhost:44393/api/car/" + Id;
                var content = ApiHelper. CreateHttpContent(entity);
                var result = await client.PutAsync(Endpoint, content);
                string resultContent = await result.Content.ReadAsStringAsync();
            }
            return 1;
        }

        public async Task<Car> FindByIdAsync(object Id)
        {
            using (var client = new HttpClient())
            {
                var UserToken = _session.GetString("UserToken");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + UserToken);
                client.DefaultRequestHeaders.Add("x-version", "1.0");
                var Endpoint = "https://localhost:44393/api/car/" + Id;
                var result = await client.GetAsync(Endpoint);
                string resultContent = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Car>(resultContent);
            }

        }
        public async Task<List<Car>> ListAsync()
        {
            using (var client = new HttpClient())
            {
                var UserToken = _session.GetString("UserToken");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + UserToken);
                client.DefaultRequestHeaders.Add("x-version", "1.0");
                var Endpoint = "https://localhost:44393/api/car";
                var result = await client.GetAsync(Endpoint);
                string resultContent = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Car>>(resultContent);
            }

        }

        public void Add(Car entity)
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

        public Car FindById(object Id)
        {
            throw new NotImplementedException();
        }

      

        public List<Car> List()
        {
            throw new NotImplementedException();
        }
 

        public Task<List<Car>> ListAsync(object JournalType, object SubledgerTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ObjectExists(object Id)
        {
            throw new NotImplementedException();
        }

        public List<Car> Search(object term)
        {
            throw new NotImplementedException();
        }

        public void Update(object Id, Car entity)
        {
            throw new NotImplementedException();
        }

      
    }
}
