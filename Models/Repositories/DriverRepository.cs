
using OilStationMVC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class DriverRepository : IMainRepository<Driver>
    {
        DataBase.DB db;
        public DriverRepository()
        {
            db = new DataBase.DB();
        } 

        public async Task<bool> AddAsync(Driver entity)
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
         
        public async Task<Driver> FindByIdAsync(object Id)
        {
            string sql = " select * from Driver where nId=" + Id + "";
            DataTable dt = await db.GetDataTableAsync(sql);
            if (dt.Rows.Count > 0)
            {
                return new Driver
                {
                    Id = ObjectConverter.CInt(dt.Rows[0]["nId"]),
                    Name = ObjectConverter.Cstr(dt.Rows[0]["sName"]),
                    Tel = ObjectConverter.Cstr(dt.Rows[0]["sTel"]),
                    Desc = ObjectConverter.Cstr(dt.Rows[0]["sDesc"]),

                };
            }
            return null;
        }
         
        public async Task<List<Driver>> ListAsync()
        {
            string sql = " select * from Driver ";
            DataTable dt = await db.GetDataTableAsync(sql);
            List<Driver> lst = new List<Driver>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new Driver
                    {
                        Id = ObjectConverter.CInt(row["nId"]),
                        Name = ObjectConverter.Cstr(row["sName"]),
                        Tel = ObjectConverter.Cstr(row["sTel"]),
                        Desc = ObjectConverter.Cstr(row["sDesc"]),

                    });
                }
            }
            return lst;
        }
         

        public async Task<bool> ObjectExists(object Id)
        {
            Driver entity = await FindByIdAsync(Id);
            if (entity != null)
                return true;
            return false;
        }

        public List<Driver> Search(object term)
        {
            throw new NotImplementedException();
        } 

        public async Task<bool> UpdateAsync(object Id, Driver entity)
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

        public bool ExecInsert(Driver entity)
        {
            try
            {
                db.NewFields();
                db.Table = "Driver";
                entity.Id = int.Parse(db.GetNewKey("nId", "Driver"));
                db.AddField("nId", entity.Id.ToString());
                db.AddField("nBranchId", CommonFolder.Common.Current.BranchId.ToString());
                db.AddFieldstr("sName", ObjectConverter.Cstr(entity.Name));
                db.AddFieldstr("sTel", ObjectConverter.Cstr(entity.Tel));
                db.AddFieldstr("sDesc", ObjectConverter.Cstr(entity.Desc));
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
                db.Table = "Driver";
                db.sCondition = " nId=" + Id + "";
                db.ExecDeleteInTrans();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool ExecUpdate(object Id, Driver entity) 
        {
            try
            {
                db.NewFields();
                db.Table = "Driver";
                db.AddFieldstr("sName", ObjectConverter.Cstr(entity.Name));
                db.AddFieldstr("sTel", ObjectConverter.Cstr(entity.Tel));
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
    }
}
