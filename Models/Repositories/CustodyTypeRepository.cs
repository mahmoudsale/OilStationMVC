
using OilStationMVC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class CustodyTypeRepository:IMainRepository<CustodyType>
    {
        private readonly DataBase.DB db;

        public CustodyTypeRepository()
        {
            this.db = new DataBase.DB();
        }
 
        public async Task<bool> AddAsync(CustodyType entity)
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
         
        public async Task<CustodyType> FindByIdAsync(object Id)
        {
            string sql = " select * from OilCustodyType where nId=" + Id + "";
            DataTable dt =await db.GetDataTableAsync(sql);
            if (dt.Rows.Count > 0)
            {
                return new CustodyType
                {
                    Id = ObjectConverter.CInt(dt.Rows[0]["nId"]),
                    TypeName = ObjectConverter.Cstr(dt.Rows[0]["sName"]),

                };
            }
            return null;
        }
         
        public async Task<List<CustodyType>> ListAsync()
        {
            string sql = " select * from OilCustodyType ";
            DataTable dt =await db.GetDataTableAsync(sql);
            List<CustodyType> lst = new List<CustodyType>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new CustodyType
                    {
                        Id = ObjectConverter.CInt(row["nId"]),
                        TypeName = ObjectConverter.Cstr(row["sName"]),

                    });
                }
            }
            return lst;
        }
 

        public async Task<bool> ObjectExists(object Id)
        {
            CustodyType entity = await FindByIdAsync(Id);
            if (entity != null)
                return true;
            return false;
        }

        public List<CustodyType> Search(object term)
        {
            throw new NotImplementedException();
        }
 
        public async Task<bool> UpdateAsync(object Id, CustodyType entity)
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

        public bool ExecInsert(CustodyType entity)
        {
            try
            {
                db.NewFields();
                db.Table = "OilCustodyType";
                entity.Id = int.Parse(db.GetNewKey("nId", "OilCustodyType"));
                db.AddField("nId", entity.Id.ToString());
                db.AddFieldstr("sName", entity.TypeName.ToString());
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
                db.Table = "OilCustodyType";
                db.sCondition = " nId=" + Id + "";
                db.ExecDeleteInTrans();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool ExecUpdate(object Id, CustodyType entity)
        {
            try
            {
                db.NewFields();
                db.Table = "OilCustodyType";
                db.AddFieldstr("sName", entity.TypeName.ToString());
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
