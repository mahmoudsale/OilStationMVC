
using OilStationMVC.Helper;
using OilStationMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class CustomerInvoiceRepository
    {
        private readonly DataBase.DB db;

        public CustomerInvoiceRepository()
        {
            this.db = new DataBase.DB();
        }

        public async Task<bool> AddAsync(CustomerInvoice entity)
        {
            try
            {
                ExecInsert(entity);
                UpdateBalance(true, entity.SolarQTY, entity.Oil80QTY, entity.Oil92QTY, entity.Oil95QTY);
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

        public bool ExecInsert(CustomerInvoice entity)
        {
            try
            {
                db.NewFields();
                db.Table = "InnvoiceCustomer";
                entity.Id = int.Parse( db.GetNewKey("nId", "InnvoiceCustomer", "nBranchId=" + CommonFolder.Common.Current.BranchId + ""));
                db.AddField("nId", entity.Id.ToString());
                db.AddField("nBranchId", CommonFolder.Common.Current.BranchId.ToString());
                db.AddField("JournalType", ObjectConverter.Cstr(entity.JournalType));
                db.AddFieldstr("dDate", ObjectConverter.Cstr(entity.dDate.ToString("yyyy-MM-dd")));
                db.AddField("nCustNo", ObjectConverter.Cstr(entity.CustomerId));
                db.AddField("nSolar", ObjectConverter.Cstr(entity.SolarQTY));
                db.AddField("nOil80", ObjectConverter.Cstr(entity.Oil80QTY));
                db.AddField("nOil92", ObjectConverter.Cstr(entity.Oil92QTY));
                db.AddField("nOil95", ObjectConverter.Cstr(entity.Oil95QTY));
                db.AddFieldG("SolarUnitPrice", ObjectConverter.Cstr(entity.SolarUnitPrice));
                db.AddFieldG("Oil80UnitPrice", ObjectConverter.Cstr(entity.Oil80TotalPrice));
                db.AddFieldG("Oil92UnitPrice", ObjectConverter.Cstr(entity.Oil92UnitPrice));
                db.AddFieldG("Oil95UnitPrice", ObjectConverter.Cstr(entity.Oil95UnitPrice));
                db.AddFieldG("SolarTotalPrice", ObjectConverter.Cstr(entity.SolarTotalPrice));
                db.AddFieldG("Oil80TotalPrice", ObjectConverter.Cstr(entity.Oil80TotalPrice));
                db.AddFieldG("Oil92TotalPrice", ObjectConverter.Cstr(entity.Oil92TotalPrice));
                db.AddFieldG("Oil95TotalPrice", ObjectConverter.Cstr(entity.Oil95TotalPrice));
                db.AddFieldG("nPrice", ObjectConverter.Cstr(entity.TotalPrice));
                db.ExecInsertInTrans();
            }
            catch (Exception ex)
            {
                return false; ;
            }
            return true;
        }

        public void UpdateBalance(bool InsertOrUpdate, decimal Solar, decimal Oil80, decimal Oil92, decimal Oil95)
        {
            if (InsertOrUpdate)
            {
                string sql = "update ProductMaster set nFirstBalanceperiod=nFirstBalanceperiod +" + Solar + " where nId=1 ";
                sql += "update ProductMaster set nFirstBalanceperiod=nFirstBalanceperiod +" + Oil80 + " where nId=2 ";
                sql += "update ProductMaster set nFirstBalanceperiod=nFirstBalanceperiod +" + Oil92 + " where nId=3 ";
                sql += "update ProductMaster set nFirstBalanceperiod=nFirstBalanceperiod +" + Oil95 + " where nId=4 ";
                db.ExecutSQLInTrans(sql);
            }
            else
            {
                string sql = "update ProductMaster set nFirstBalanceperiod=nFirstBalanceperiod -" + Solar + " where nId=1 ";
                sql += "update ProductMaster set nFirstBalanceperiod=nFirstBalanceperiod -" + Oil80 + " where nId=2 ";
                sql += "update ProductMaster set nFirstBalanceperiod=nFirstBalanceperiod -" + Oil92 + " where nId=3 ";
                sql += "update ProductMaster set nFirstBalanceperiod=nFirstBalanceperiod -" + Oil95 + " where nId=4 ";
                db.ExecutSQLInTrans(sql);
            }
        }

        public async Task<bool> DeleteByIdAsync(object Id)
        {
            try
            {
                ExecDelete(Id);
                CustomerInvoice invoice = await FindByIdAsync(Id);
                UpdateBalance(false, invoice.SolarQTY, invoice.Oil80QTY, invoice.Oil92QTY, invoice.Oil95QTY);
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
                db.Table = "InnvoiceCustomer";
                db.sCondition = " nId=" + Id + "";
                db.ExecDeleteInTrans();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<CustomerInvoice> FindByIdAsync(object Id)
        {
            string sql = " select * from InnvoiceCustomer where nId=" + Id + " and nBranchId=" + CommonFolder.Common.Current.BranchId + " ";
            DataSet ds = await db.GetDataSetAsync(sql);
            DataTable dt = ds.Tables[0];
            CustomerInvoice invoice = new CustomerInvoice();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {

                    invoice.Id = ObjectConverter.CInt(row["nId"]);
                    invoice.dDate = ObjectConverter.CDate(row["dDate"]);
                    invoice.BranchId = ObjectConverter.CInt(row["nBranchId"]);
                    invoice.CustomerId = ObjectConverter.CInt(row["nCustNo"]);
                    invoice.TotalPrice = ObjectConverter.CDec(row["nPrice"]);
                    invoice.SolarQTY = ObjectConverter.CInt(row["nSolar"]);
                    invoice.Oil80QTY = ObjectConverter.CInt(row["nOil80"]);
                    invoice.Oil92QTY = ObjectConverter.CInt(row["nOil92"]);
                    invoice.Oil95QTY = ObjectConverter.CInt(row["nOil95"]);
                    invoice.SolarUnitPrice = ObjectConverter.CDec(row["SolarUnitPrice"]);
                    invoice.Oil80UnitPrice = ObjectConverter.CDec(row["Oil80UnitPrice"]);
                    invoice.Oil92UnitPrice = ObjectConverter.CDec(row["Oil92UnitPrice"]);
                    invoice.Oil95UnitPrice = ObjectConverter.CDec(row["Oil95UnitPrice"]);
                    invoice.SolarTotalPrice = ObjectConverter.CDec(row["SolarTotalPrice"]);
                    invoice.Oil80TotalPrice = ObjectConverter.CDec(row["Oil80TotalPrice"]);
                    invoice.Oil92TotalPrice = ObjectConverter.CDec(row["Oil92TotalPrice"]);
                    invoice.Oil95TotalPrice = ObjectConverter.CDec(row["Oil95TotalPrice"]);
                     
                }
                return invoice;
            }
            else
            {
                return null;
            }

        }

        public async Task<List<CustomerInvoiceViewModel>> ListAsync()
        {
            string sql = " select * from vuw_StationCustomerInnvoices where nBranchId=" + CommonFolder.Common.Current.BranchId + " ";
            DataSet ds = await db.GetDataSetAsync(sql);
            DataTable dt = ds.Tables[0];
            List<CustomerInvoiceViewModel> lst = new List<CustomerInvoiceViewModel>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new CustomerInvoiceViewModel
                    {
                        Id = ObjectConverter.CInt(row["nId"]),
                        dDate = ObjectConverter.CDate(row["dDate"]),
                        BranchId = ObjectConverter.CInt(row["nBranchId"]),
                        CustomerId = ObjectConverter.CInt(row["nCustNo"]),
                        Customer = new Customer
                        {
                            Id = ObjectConverter.CInt(row["nCustNo"]),
                            Name = ObjectConverter.Cstr(row["sName"]),
                        },
                        TotalPrice = ObjectConverter.CDec(row["nPrice"]),

                    });
                }
            }
            return lst;
        }

        public Task<bool> ObjectExists(object Id)
        {
            throw new NotImplementedException();
        }

        public List<CustomerInvoice> Search(object term)
        {
            throw new NotImplementedException();
        } 

        public async Task<bool> UpdateAsync(object Id, CustomerInvoice entity)
        {
            try
            {
                ExecUpdate(Id, entity);
                CustomerInvoice OldInvoice = await FindByIdAsync(Id);
                UpdateBalance(true, OldInvoice.SolarQTY, OldInvoice.Oil80QTY, OldInvoice.Oil92QTY, OldInvoice.Oil95QTY);
                UpdateBalance(false, entity.SolarQTY, entity.Oil80QTY, entity.Oil92QTY, entity.Oil95QTY);
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

        public bool ExecUpdate(object Id,CustomerInvoice entity)
        {
            try
            {
                db.NewFields();
                db.Table = "InnvoiceCustomer";
                db.AddFieldstr("dDate", ObjectConverter.Cstr(entity.dDate.ToString("yyyy-MM-dd")));
                db.AddField("nCustNo", ObjectConverter.Cstr(entity.CustomerId));
                db.AddField("nSolar", ObjectConverter.Cstr(entity.SolarQTY));
                db.AddField("nOil80", ObjectConverter.Cstr(entity.Oil80QTY));
                db.AddField("nOil92", ObjectConverter.Cstr(entity.Oil92QTY));
                db.AddField("nOil95", ObjectConverter.Cstr(entity.Oil95QTY));
                db.AddFieldG("SolarUnitPrice", ObjectConverter.Cstr(entity.SolarUnitPrice));
                db.AddFieldG("Oil80UnitPrice", ObjectConverter.Cstr(entity.Oil80UnitPrice));
                db.AddFieldG("Oil92UnitPrice", ObjectConverter.Cstr(entity.Oil92UnitPrice));
                db.AddFieldG("Oil95UnitPrice", ObjectConverter.Cstr(entity.Oil95UnitPrice));
                db.AddFieldG("SolarTotalPrice", ObjectConverter.Cstr(entity.SolarTotalPrice));
                db.AddFieldG("Oil80TotalPrice", ObjectConverter.Cstr(entity.Oil80TotalPrice));
                db.AddFieldG("Oil92TotalPrice", ObjectConverter.Cstr(entity.Oil92TotalPrice));
                db.AddFieldG("Oil95TotalPrice", ObjectConverter.Cstr(entity.Oil95TotalPrice));
                db.AddFieldG("nPrice", ObjectConverter.Cstr(entity.TotalPrice));
                db.sCondition = "nId=" + Id + " and nBranchId=" + CommonFolder.Common.Current.BranchId + "";
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
