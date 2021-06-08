
using Newtonsoft.Json;
using OilStationMVC.CommonFolder;
using OilStationMVC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
namespace OilStationMVC.Models.Repositories
{
    public class CarRepository : IMainRepository<Car>
    {
        DataBase.DB db = null;
        public CarRepository()
        {
            db = new DataBase.DB();
        } 
        public async Task<bool> AddAsync(Car entity)
        {
            try
            {
                ExecInsert(entity);
                DataBase.ActionResult ActionR = await db.Apply();
                if (ActionR.Success == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
         
        public async Task<bool> DeleteByIdAsync(object Id)
        {
            try
            {
                ExecDelete(Id);
                DataBase.ActionResult ActionR = await db.Apply();
                if (ActionR.Success == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
         
        public async Task<Car> FindByIdAsync(object Id)
        {
            string sql = " select * from Cars where nId=" + Id + "";
           
            DataTable dt = await db.GetDataTableAsync(sql);
            if (dt.Rows.Count > 0)
            {
                return new Car
                {
                    Id = ObjectConverter.CInt(dt.Rows[0]["nId"]),
                    Name = ObjectConverter.Cstr(dt.Rows[0]["sName"]), 
                    Desc = ObjectConverter.Cstr(dt.Rows[0]["sDesc"]),
                    DriverId= ObjectConverter.CInt(dt.Rows[0]["nDriverNo"]), 
                };
            }
            return null;
        }

        public async Task<List<Car>> ListAsync()
        {
            string sql = " select * from Cars ";
            DataTable dt = await db.GetDataTableAsync(sql);
            List<Car> lst = new List<Car>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new Car
                    {
                        Id = ObjectConverter.CInt(row["nId"]),
                        Name = ObjectConverter.Cstr(row["sName"]),
                        Desc = ObjectConverter.Cstr(row["sDesc"]),
                        Driver = new Driver
                        {
                            Id = ObjectConverter.CInt(row["nId"]),
                            Name = ObjectConverter.Cstr(row["sName"]),
                        }
                    });
                }
            }
            return lst;
        }

        //public async Task<List<Car>> ListAsync()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var UserToken = _session.GetString("UserToken");
        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + UserToken);
        //        client.DefaultRequestHeaders.Add("x-version", "1.0");
        //        var Endpoint = "https://localhost:44393/api/car";
        //        var result = await client.GetAsync(Endpoint);
        //        string resultContent = await result.Content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject<List<Car>>(resultContent);
        //    }
        //}

        public async Task<bool> ObjectExists(object Id)
        {
            Car entity = await FindByIdAsync(Id);
            if (entity != null)
                return true;
            return false;
        }

        public List<Car> Search(object term)
        {
            throw new NotImplementedException();
        }
         
        public async Task<bool> UpdateAsync(object Id, Car entity)
        {
            try
            {
                ExecUpdate(Id, entity);
                DataBase.ActionResult ActionR = await db.Apply();
                if (ActionR.Success == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ExecInsert(Car entity)
        {
            try
            {
                db.NewFields();
                db.Table = "Cars";
                entity.Id = int.Parse(db.GetNewKey("nId", "Cars"));
                db.AddField("nId", entity.Id.ToString());
                db.AddField("nBranchId", Common.Current.BranchId.ToString());
                db.AddFieldstr("sName", ObjectConverter.Cstr(entity.Name));
                db.AddField("nDriverNo", ObjectConverter.Cstr(entity.DriverId));
                db.ExecInsertInTrans();
            }
            catch (Exception ex)
            {
                return false; ;
            }
            return true;
        }

        public bool ExecDelete(object Id)
        {
            try
            {
                db.NewFields();
                db.Table = "Cars";
                db.sCondition = " nId=" + Id + "";
                db.ExecDeleteInTrans();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool ExecUpdate(object Id, Car entity)
        {
            try
            {
                db.NewFields();
                db.Table = "Cars";
                db.AddFieldstr("sName", ObjectConverter.Cstr(entity.Name));
                db.AddField("nDriverNo", ObjectConverter.Cstr(entity.DriverId));
                db.sCondition = " nId=" + Id + " ";
                db.ExecUpdateInTrans();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
