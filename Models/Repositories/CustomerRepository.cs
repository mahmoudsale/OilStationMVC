
using OilStationMVC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class CustomerRepository : IMainRepository<Customer>
    {
        private readonly DataBase.DB db;

        public CustomerRepository( )
        {
            this.db = new DataBase.DB ();
        }
         
        public async Task<bool> AddAsync(Customer entity)
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
         
        public async Task<Customer> FindByIdAsync(object Id)
        {
            string sql = " select * from Customer where nId=" + Id + "";
            DataTable dt =await db.GetDataTableAsync(sql);
            if (dt.Rows.Count > 0)
            {
                return new Customer
                {
                    Id = ObjectConverter.CInt(dt.Rows[0]["nId"]),
                    Name = ObjectConverter.Cstr(dt.Rows[0]["sName"]), 
                    Adress = ObjectConverter.Cstr(dt.Rows[0]["sAdress"]), 
                    Desc = ObjectConverter.Cstr(dt.Rows[0]["sDesc"]),
                    CustomerTypeId= ObjectConverter.CInt(dt.Rows[0]["nType"]),
                    Email= ObjectConverter.Cstr(dt.Rows[0]["sMail"]),
                    MazotUnitPrice=1920,
                    
                };
            }
            return null;
        }
         
        public async Task<List<Customer>> ListAsync()
        {
            string sql = " select * from vuw_AllCustomer ";
            DataTable dt =await db.GetDataTableAsync(sql);
            List<Customer> lst = new List<Customer>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new Customer
                    {
                        Id = ObjectConverter.CInt(row["nId"]),
                        Name = ObjectConverter.Cstr(row["sName"]), 
                        Adress = ObjectConverter.Cstr(row["sAdress"]), 
                        Desc = ObjectConverter.Cstr(row["sDesc"]),
                        CustomerType=new CustomerType
                        {
                            Id= ObjectConverter.CInt(row["ntype"]),
                            Name= ObjectConverter.Cstr(row["CustomerType"]),
                        }
                    });
                }
            }
            return lst;
        }
 

        public async Task<bool> ObjectExists(object Id)
        {
            Customer entity = await FindByIdAsync(Id);
            if (entity != null)
                return true;
            return false;
        }

        public List<Customer> Search(object term)
        {
            throw new NotImplementedException();
        }
          
        public async Task<bool> UpdateAsync(object Id, Customer entity)
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

        public bool ExecInsert(Customer entity)
        {
            try
            {
                db.NewFields();
                db.Table = "Customer";
                entity.Id = int.Parse( db.GetNewKey("nId", "Customer"));
                db.AddField("nId", entity.Id.ToString());
                db.AddField("nBranchId", CommonFolder.Common.Current.BranchId.ToString());
                db.AddFieldstr("sName", ObjectConverter.Cstr(entity.Name));
                db.AddFieldstr("sAdress", ObjectConverter.Cstr(entity.Adress));
                db.AddFieldstr("ntype", ObjectConverter.Cstr(entity.CustomerTypeId));
                db.AddFieldstr("sTel", ObjectConverter.Cstr(entity.Telephone));
                db.AddFieldstr("sFax", ObjectConverter.Cstr(entity.Fax));
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
                db.Table = "Customer";
                db.sCondition = " nId=" + Id + "";
                db.ExecDeleteInTrans();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool ExecUpdate(object Id, Customer entity) 
        {
            try
            {
                db.NewFields();
                db.Table = "Customer";
                db.AddFieldstr("sName", ObjectConverter.Cstr(entity.Name));
                db.AddFieldstr("sAdress", ObjectConverter.Cstr(entity.Adress));
                db.AddFieldstr("ntype", ObjectConverter.Cstr(entity.CustomerTypeId));
                db.AddFieldstr("sTel", ObjectConverter.Cstr(entity.Telephone));
                db.AddFieldstr("sFax", ObjectConverter.Cstr(entity.Fax));
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
