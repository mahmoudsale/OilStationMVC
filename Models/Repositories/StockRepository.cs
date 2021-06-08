using OilStationMVC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class StockRepository : IMainRepository<Stock>
    {
        DataBase.DB db;
        public StockRepository()
        {
            db = new DataBase.DB();
        }

        public async Task<bool> AddAsync(Stock entity)
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
         
        public async Task<Stock> FindByIdAsync(object Id)
        {
            string sql = " select * from Stocks where nId=" + Id + "";
            DataSet ds = await db.GetDataSetAsync(sql);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                return new Stock
                {
                    Id = ObjectConverter.CInt(dt.Rows[0]["nId"]),
                    Name = ObjectConverter.Cstr(dt.Rows[0]["sName"]),
                    NameEng = ObjectConverter.Cstr(dt.Rows[0]["sNameEng"]),
                    Adress = ObjectConverter.Cstr(dt.Rows[0]["sAdress"]),
                    Cost = ObjectConverter.CDec(dt.Rows[0]["nCost"]),
                    Desc = ObjectConverter.Cstr(dt.Rows[0]["sDesc"]),
                };
            }
            return null;
        }

        public async Task<List<Stock>> ListAsync()
        {
            string sql = " select * from Stocks ";
            DataTable dt = await db.GetDataTableAsync(sql);
            List<Stock> lst = new List<Stock>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new Stock
                    {
                        Id = ObjectConverter.CInt(row["nId"]),
                        Name = ObjectConverter.Cstr(row["sName"]),
                        NameEng = ObjectConverter.Cstr(row["sNameEng"]),
                        Adress = ObjectConverter.Cstr(row["sAdress"]),
                        Cost = ObjectConverter.CDec(row["nCost"]),
                        Desc = ObjectConverter.Cstr(row["sDesc"]),
                    });
                }
            }
            return lst;
        }

        public async Task<bool> ObjectExists(object Id)
        {
            Stock entity = await FindByIdAsync(Id);
            if (entity != null)
                return true;
            return false;
        }

        public List<Stock> Search(object term)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(object Id, Stock entity)
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

        public bool ExecInsert(Stock entity)
        {
            try
            {
                db.NewFields();
                db.Table = "Stocks";
                entity.Id = int.Parse(db.GetNewKey("nId", "Stocks"));
                db.AddField("nId", entity.Id.ToString());
                db.AddField("nBranchId", CommonFolder.Common.Current.BranchId.ToString());
                db.AddFieldstr("sName", ObjectConverter.Cstr(entity.Name));
                db.AddFieldstr("sNameEng", ObjectConverter.Cstr(entity.NameEng));
                db.AddFieldstr("sAdress", ObjectConverter.Cstr(entity.Adress));
                db.AddFieldG("nCost", ObjectConverter.Cstr(entity.Cost));
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
                db.Table = "Stocks";
                db.sCondition = " nId=" + Id + "";
                db.ExecDeleteInTrans();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool ExecUpdate(object Id, Stock entity)

        {
            try
            {
                db.NewFields();
                db.Table = "Stocks";
                db.AddFieldstr("sName", ObjectConverter.Cstr(entity.Name));
                db.AddFieldstr("sNameEng", ObjectConverter.Cstr(entity.NameEng));
                db.AddFieldstr("sAdress", ObjectConverter.Cstr(entity.Adress));
                db.AddFieldG("nCost", ObjectConverter.Cstr(entity.Cost));
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
