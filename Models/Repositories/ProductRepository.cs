
using OilStationMVC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class ProductRepository:IMainRepository<Product>
    {
        DataBase.DB db;

        public ProductRepository()
        {
            db = new DataBase.DB();
        } 

        public async Task<bool> AddAsync(Product entity)
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
         
        public async Task<Product> FindByIdAsync(object Id)
        {
            string sql = " select * from ProductMaster where nId=" + Id + " ";
            DataTable dt = await db.GetDataTableAsync(sql);
            List<Product> lst = new List<Product>();
            if (dt.Rows.Count > 0)
            {
                return new Product
                {
                    Id = ObjectConverter.CInt(dt.Rows[0]["nId"]),
                    Name = ObjectConverter.Cstr(dt.Rows[0]["sName"]),
                    BuyUnitPrice = ObjectConverter.CDec(dt.Rows[0]["nBuyrice"]),
                    SaleUnitPrice = ObjectConverter.CDec(dt.Rows[0]["nSalePrice"]),
                    FirstPeriodBalance = ObjectConverter.CDec(dt.Rows[0]["nFirstBalanceperiod"]),
                };
            }
            else
            {
                return null;
            }
        }
         
        public async Task<List<Product>> ListAsync()
        {
            string sql = " select * from ProductMaster ";
            DataTable dt = await db.GetDataTableAsync(sql);
            List<Product> lst = new List<Product>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new Product
                    {
                        Id = ObjectConverter.CInt(row["nId"]),
                        Name = ObjectConverter.Cstr(row["sName"]),
                        BuyUnitPrice = ObjectConverter.CDec(row["nBuyrice"]),
                        SaleUnitPrice = ObjectConverter.CDec(row["nSalePrice"]),
                        FirstPeriodBalance = ObjectConverter.CDec(row["nFirstBalanceperiod"]),
                    });
                }
            }
            return lst;
        }
 
        public Task<bool> ObjectExists(object Id)
        {
            throw new NotImplementedException();
        }

        public List<Product> Search(object term)
        {
            throw new NotImplementedException();
        }
         
        public async Task<bool> UpdateAsync(object Id, Product entity)
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

        public bool ExecInsert(Product entity)
        {
            try
            {
                db.NewFields();
                db.Table = " ProductMaster";
                entity.Id = int.Parse(db.GetNewKey("nId", "Stocks"));
                db.AddField("nId", entity.Id.ToString());
                db.AddFieldstr("sName", ObjectConverter.Cstr(entity.Name));
                db.AddFieldG("nBuyrice", ObjectConverter.Cstr(entity.BuyUnitPrice));
                db.AddFieldG("nSalePrice", ObjectConverter.Cstr(entity.SaleUnitPrice));
                db.AddFieldG("nFirstBalanceperiod", ObjectConverter.Cstr(entity.FirstPeriodBalance));
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
                db.Table = "ProductMaster";
                db.sCondition = " nId=" + Id + "";
                db.ExecDeleteInTrans();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool ExecUpdate(object Id, Product entity)
        {
            try
            {
                db.NewFields();
                db.Table = " ProductMaster";
                db.AddFieldstr("sName", ObjectConverter.Cstr(entity.Name));
                db.AddFieldG("nBuyrice", ObjectConverter.Cstr(entity.BuyUnitPrice));
                db.AddFieldG("nSalePrice", ObjectConverter.Cstr(entity.SaleUnitPrice));
                db.AddFieldG("nFirstBalanceperiod", ObjectConverter.Cstr(entity.FirstPeriodBalance));
                db.sCondition = " nId=" + Id + "";
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
