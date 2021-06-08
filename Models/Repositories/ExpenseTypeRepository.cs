
using OilStationMVC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class ExpenseTypeRepository : IMainRepository<ExpenseType>
    {

        DataBase.DB db;
        public ExpenseTypeRepository()
        {
            db =new DataBase.DB ();
        }
          
        public async Task<bool> AddAsync(ExpenseType entity)
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
         
        public async Task<ExpenseType> FindByIdAsync(object Id)
        {
            string sql = " select * from Expensestype where nId=" + Id + "";
            DataTable dt =await db.GetDataTableAsync(sql);
            if (dt.Rows.Count > 0)
            {
                return new ExpenseType
                {
                    Id = ObjectConverter.CInt(dt.Rows[0]["nId"]),
                    TypeName = ObjectConverter.Cstr(dt.Rows[0]["sName"]), 

                };
            }
            return null;
        }
         
        public async Task<List<ExpenseType>> ListAsync()
        {
            string sql = " select * from Expensestype ";
            DataTable dt =await db.GetDataTableAsync(sql);
            List<ExpenseType> lst = new List<ExpenseType>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new ExpenseType
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
            ExpenseType entity = await FindByIdAsync(Id);
            if (entity != null)
                return true;
            return false;
        }

        public List<ExpenseType> Search(object term)
        {
            throw new NotImplementedException();
        }
         
        public async Task<bool> UpdateAsync(object Id, ExpenseType entity)
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

        public bool ExecInsert(ExpenseType entity)
        {
            try
            {
                db.NewFields();
                db.Table = "Expensestype";
                entity.Id = int.Parse(db.GetNewKey("nId", "Expensestype"));
                db.AddField("nId", entity.Id.ToString());
                db.AddField("nBranchId", CommonFolder.Common.Current.BranchId.ToString());
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
                db.Table = "Expensestype";
                db.sCondition = " nId=" + Id + "";
                db.ExecDeleteInTrans();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool ExecUpdate(object Id, ExpenseType entity)

        {
            try
            {
                db.NewFields();
                db.Table = "Expensestype";
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
