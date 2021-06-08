
using OilStationMVC.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class SubledgerRepository
    {
        DataBase.DB _db;
        public SubledgerRepository(DataBase.DB db)
        {
            _db = db;

        }
        public void Add(Subledger entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(Subledger entity)
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

        public Subledger FindById(object Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Subledger> FindByIdAsync(object SubledgerId, object SubledgerTypeId)
        {
            List<Subledger> lst = await ListAsync();
            var subledger = lst.SingleOrDefault(A => A.Id == ObjectConverter.CInt(SubledgerId) && A.SubledgerTypeId == ObjectConverter.CInt(SubledgerTypeId));
            return subledger;
        }

        public List<Subledger> List()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Subledger>> ListAsync(object SubledgerTypeId = null)
        {
            List<Subledger> lst = new List<Subledger>();
            List<Customer> customers = await _db.Customers.FromSql("select *  from  Customers").ToListAsync<Customer>();
            List<Car> cars = await _db.Cars.FromSql("select * from  Cars").ToListAsync<Car>();
            List<Company> Companies = await _db.Companies.FromSql("select * from  Companies").ToListAsync<Company>();
            List<ExpenseType> ExpenseTypes = await _db.ExpenseTypes.FromSql("select * from  ExpenseTypes").ToListAsync<ExpenseType>();

            if (customers.Any())
            {
                foreach (var cus in customers)
                {
                    Subledger subledger = new Subledger
                    {
                        Id = cus.Id,
                        Name = cus.Name,
                        SubledgerTypeId = cus.SubledgerTypeId
                    };
                    lst.Add(subledger);
                }

            }
            if (cars.Any())
            {
                foreach (var car in cars)
                {
                    Subledger subledger = new Subledger
                    {
                        Id = car.Id,
                        Name = car.Name,
                        SubledgerTypeId = car.SubledgerTypeId
                    };
                    lst.Add(subledger);
                }

            }
            if (Companies.Any())
            {
                foreach (var company in Companies)
                {
                    Subledger subledger = new Subledger
                    {
                        Id = company.Id,
                        Name = company.Name,
                        SubledgerTypeId = company.SubledgerTypeId
                    };
                    lst.Add(subledger);
                }

            }
            if (ExpenseTypes.Any())
            {
                foreach (var type in ExpenseTypes)
                {
                    Subledger subledger = new Subledger
                    {
                        Id = type.Id,
                        Name = type.TypeName.ToString(),
                        SubledgerTypeId = 4
                    };
                    lst.Add(subledger);
                }

            }


            if (string.IsNullOrEmpty(ObjectConverter.Cstr(SubledgerTypeId)))
            {
                return lst;
            }
            else
            {
                List<Subledger> lst2 = lst.Where(a => a.SubledgerTypeId == ObjectConverter.CInt(SubledgerTypeId)).ToList();

                return lst2;
            }

        }

        public List<Subledger> Search(object term)
        {
            throw new NotImplementedException();
        }

        public void Update(object Id, Subledger entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(object Id, Subledger entity)
        {
            throw new NotImplementedException();
        }
    }
}
