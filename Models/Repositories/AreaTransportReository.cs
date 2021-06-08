
using Newtonsoft.Json;
using OilStationMVC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class AreaTransportReository : IMainRepository<AreaTransport>
    {
        private readonly DataBase.DB db;

        public AreaTransportReository()
        {
            this.db = new DataBase.DB();
        }

        public async Task<bool> AddAsync(AreaTransport entity)
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

        public bool ExecInsert(AreaTransport entity)
        {
            try
            {
                db.NewFields();
                db.Table = "AreaTransport";
                entity.Id = int.Parse(db.GetNewKey("nId", "AreaTransport"));
                db.AddField("nId", entity.Id.ToString());
                db.AddField("nBranchId", CommonFolder.Common.Current.BranchId.ToString());
                db.AddFieldstr("sName", ObjectConverter.Cstr(entity.Name));
                db.AddFieldstr("sNameEng", ObjectConverter.Cstr(entity.NameEng));
                db.AddFieldstr("sAdress", ObjectConverter.Cstr(entity.Adress));
                db.AddFieldstr("sDesc", ObjectConverter.Cstr(entity.Desc));
                db.AddFieldG("nCost", ObjectConverter.Cstr(entity.Cost));
                db.ExecInsertInTrans();
            }
            catch (Exception ex)
            {
                return false; ;
            }
            return true;
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

        public async Task<AreaTransport> FindByIdAsync(object Id)
        {
            string sql = " select * from AreaTransport where nId=" + Id + "";
            DataTable dt = await db.GetDataTableAsync(sql);
            if (dt.Rows.Count > 0)
            {
                return new AreaTransport
                {
                    Id = ObjectConverter.CInt(dt.Rows[0]["nId"]),
                    Name = ObjectConverter.Cstr(dt.Rows[0]["sName"]),
                    NameEng = ObjectConverter.Cstr(dt.Rows[0]["sNameEng"]),
                    Adress = ObjectConverter.Cstr(dt.Rows[0]["sAdress"]),
                    Cost = ObjectConverter.CDec(dt.Rows[0]["nCost"]),
                    Desc = ObjectConverter.Cstr(dt.Rows[0]["sDesc"]),
                    BranchId = ObjectConverter.CInt(dt.Rows[0]["nBranchId"])
                };
            }
            return null;
        }

        public async Task<List<AreaTransport>> ListAsync()
        {
            string sql = " select * from AreaTransport ";
            DataTable dt = await db.GetDataTableAsync(sql);
            List<AreaTransport> lst = new List<AreaTransport>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new AreaTransport
                    {
                        Id = ObjectConverter.CInt(dt.Rows[0]["nId"]),
                        Name = ObjectConverter.Cstr(dt.Rows[0]["sName"]),
                        NameEng = ObjectConverter.Cstr(dt.Rows[0]["sNameEng"]),
                        Adress = ObjectConverter.Cstr(dt.Rows[0]["sAdress"]),
                        Cost = ObjectConverter.CDec(dt.Rows[0]["nCost"]),
                        Desc = ObjectConverter.Cstr(dt.Rows[0]["sDesc"]),
                        BranchId = ObjectConverter.CInt(dt.Rows[0]["nBranchId"])
                    });
                }
            }
            return lst;
        }

        public async Task<bool> ObjectExists(object Id)
        {
            AreaTransport entity = await FindByIdAsync(Id);
            if (entity != null)
                return true;
            return false;
        }

        public List<AreaTransport> Search(object term)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(object Id, AreaTransport entity)
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

        public bool ExecDelete(object Id)
        {
            try
            {
                db.NewFields();
                db.Table = "AreaTransport";
                db.sCondition = " nId=" + Id + "";
                db.ExecDeleteInTrans();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool ExecUpdate(object Id, AreaTransport entity)
        {
            try
            {
                db.NewFields();
                db.Table = "AreaTransport";
                db.AddFieldstr("sName", ObjectConverter.Cstr(entity.Name));
                db.AddFieldstr("sNameEng", ObjectConverter.Cstr(entity.NameEng));
                db.AddFieldstr("sAdress", ObjectConverter.Cstr(entity.Adress));
                db.AddFieldG("nCost", ObjectConverter.Cstr(entity.Cost));
                db.AddFieldstr("sDesc", ObjectConverter.Cstr(entity.Desc));

                db.sCondition = " nId=" + Id + " ";
                db.ExecUpdateInTrans();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        //public async Task<List<ChargeArea>> ListAsync()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var Endpoint = "https://localhost:44393/api/Customers";
        //        var Json = await client.GetStringAsync(Endpoint);
        //        return JsonConvert.DeserializeObject<List<ChargeArea>>(Json);
        //    }
        //}

        //public async  Task<AreaTransport> FindByIdAsync()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var Endpoint = "https://jsonplaceholder.typicode.com/posts";
        //        var Json = await client.GetStringAsync(Endpoint);
        //        return JsonConvert.DeserializeObject<AreaTransport>(Json);
        //    }
        //}

        //public async void AddAsync(AreaTransport areaTransport)
        //{

        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    var response = await client.PostAsJsonAsync(new Uri("http://localhost:4688/"), areaTransport);

        //}
    }

}