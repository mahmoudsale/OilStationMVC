using Newtonsoft.Json;
using OilStationMVC.Helper;
using OilStationMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class CompanyRepository : IMainRepository<Company>
    {
        private readonly DataBase.DB db;

        public CompanyRepository()
        {
            this.db = new DataBase.DB();
        }
         
        public async Task<bool> AddAsync(Company entity)
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
         
        public async Task<Company> FindByIdAsync(object Id)
        {
            string sql = " select * from OilCompany where nId=" + Id + "";
            DataTable dt =await db.GetDataTableAsync(sql);
            if (dt.Rows.Count > 0)
            {
                return new Company
                {
                    Id = ObjectConverter.CInt(dt.Rows[0]["nId"]),
                    Name = ObjectConverter.Cstr(dt.Rows[0]["sName"]),
                    OpeningBalance = ObjectConverter.CDec(dt.Rows[0]["nOpeningBalance"]),
                    OpeningBalanceDate = ObjectConverter.CDate(dt.Rows[0]["OpeningBalanceDate"]),
                    OpeningBalanceState = ObjectConverter.CInt(dt.Rows[0]["nOpeningBalanceState"]),
                };
            }
            return null;
        }
         
        public async Task<List<Company>> ListAsync()
        {
            string sql = " select * from OilCompany ";
            DataTable dt =await db.GetDataTableAsync(sql);
            List<Company> lst = new List<Company>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new Company
                    {
                        Id = ObjectConverter.CInt(dt.Rows[0]["nId"]),
                        Name = ObjectConverter.Cstr(dt.Rows[0]["sName"]),
                        OpeningBalance = ObjectConverter.CDec(dt.Rows[0]["nOpeningBalance"]),
                        OpeningBalanceDate = ObjectConverter.CDate(dt.Rows[0]["OpeningBalanceDate"]),
                        OpeningBalanceState = ObjectConverter.CInt(dt.Rows[0]["nOpeningBalanceState"]),
                    });
                }
            }
            return lst;
        }
         
        public async Task<bool> ObjectExists(object Id)
        {
            Company entity = await FindByIdAsync(Id);
            if (entity != null)
                return true;
            return false;
        }

        public List<Company> Search(object term)
        {
            throw new NotImplementedException();
        }
         
        public async Task<bool> UpdateAsync(object Id, Company entity)
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

        public bool ExecInsert(Company entity)
        {
            try
            {
                db.NewFields();
                db.Table = "OilCompany";
                entity.Id = int.Parse(db.GetNewKey("nId", "OilCompany"));
                db.AddField("nId", entity.Id.ToString());
                db.AddField("nBranchId", CommonFolder.Common.Current.BranchId.ToString());
                db.AddFieldstr("sName", ObjectConverter.Cstr(entity.Name));
                db.AddFieldG("nOpeningBalance", ObjectConverter.Cstr(entity.OpeningBalance));
                db.AddFieldstr("OpeningBalanceDate", ObjectConverter.Cstr(entity.OpeningBalanceDate));
                db.AddField("nOpeningBalanceState", ObjectConverter.Cstr(entity.OpeningBalanceState));
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
                db.Table = "OilCompany";
                db.sCondition = " nId=" + Id + "";
                db.ExecDeleteInTrans();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool ExecUpdate(object Id, Company entity)
        {
            try
            {
                db.NewFields();
                db.Table = "OilCompany";
                db.AddFieldstr("sName", ObjectConverter.Cstr(entity.Name));
                db.AddFieldG("nOpeningBalance", ObjectConverter.Cstr(entity.OpeningBalance));
                db.AddFieldstr("OpeningBalanceDate", ObjectConverter.Cstr(entity.OpeningBalanceDate));
                db.AddField("nOpeningBalanceState", ObjectConverter.Cstr(entity.OpeningBalanceState));
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
