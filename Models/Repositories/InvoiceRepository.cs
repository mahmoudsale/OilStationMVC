
using OilStationMVC.CommonFolder;
using OilStationMVC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class InvoiceRepository
    {
        DataBase.DB db = null;
        public InvoiceRepository()
        {
            db = new DataBase.DB();
        }
        public async Task<bool> AddAsync(Invoice entity)
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

        public  bool  ExecInsert(Invoice entity)
        {
            try
            {
                db.NewFields();
                db.Table = "Innvoice";
                entity.Id = int.Parse( db.GetNewKey("nId", "Innvoice", "nBranchId=" + CommonFolder.Common.Current.BranchId + ""));
                db.AddField("nId", entity.Id.ToString());
                db.AddField("nBranchId", CommonFolder.Common.Current.BranchId.ToString());
                db.AddField("JournalType", ObjectConverter.Cstr(entity.JournalType));
                db.AddField("nCompany", ObjectConverter.Cstr(entity.CompanyId));
                db.AddField("nInnvoiceNo", ObjectConverter.Cstr(entity.InnvoiceNo));
                db.AddFieldstr("dDate", ObjectConverter.Cstr(entity.dDate.ToString("yyyy-MM-dd")));
                db.AddField("nStockNo", ObjectConverter.Cstr(entity.StockId));
                db.AddField("nStationNo", ObjectConverter.Cstr(entity.StationId));
                db.AddField("nCarNo", ObjectConverter.Cstr(entity.CarId));
                db.AddField("nDriverNo", ObjectConverter.Cstr(entity.DriverId));
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
                db.AddFieldstr("sDesc", ObjectConverter.Cstr(entity.Desc));
                db.AddFieldG("StockTransportUnit", ObjectConverter.Cstr(entity.StockTransportUnit));
                db.AddFieldG("StockTransportAmount", ObjectConverter.Cstr(entity.StockTransportAmount));
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
                Invoice invoice = await FindByIdAsync(Id);
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
                db.Table = "Innvoice";
                db.sCondition = " nId=" + Id + "";
                db.ExecDeleteInTrans();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<Invoice> FindByIdAsync(object Id)
        {
            string sql = " select * from Innvoice where nId=" + Id + " and nBranchId=" + CommonFolder.Common.Current.BranchId + " ";
            DataTable dt = await db.GetDataTableAsync(sql);
            if (dt.Rows.Count > 0)
            {
                return new Invoice
                {
                    Id = ObjectConverter.CInt(dt.Rows[0]["nId"]),
                    JournalType = ObjectConverter.CInt(dt.Rows[0]["JournalType"]),
                    BranchId = ObjectConverter.CInt(dt.Rows[0]["nBranchId"]),
                    CompanyId = ObjectConverter.CInt(dt.Rows[0]["nCompany"]),
                    InnvoiceNo = ObjectConverter.CInt(dt.Rows[0]["nInnvoiceNo"]),
                    dDate = ObjectConverter.CDate(dt.Rows[0]["dDate"]),
                    StockId = ObjectConverter.CInt(dt.Rows[0]["nStockNo"]),
                    StationId = ObjectConverter.CInt(dt.Rows[0]["nStationNo"]),
                    CarId = ObjectConverter.CInt(dt.Rows[0]["nCarNo"]),
                    DriverId = ObjectConverter.CInt(dt.Rows[0]["nDriverNo"]),
                    SolarQTY = ObjectConverter.CInt(dt.Rows[0]["nSolar"]),
                    Oil80QTY = ObjectConverter.CInt(dt.Rows[0]["nOil80"]),
                    Oil92QTY = ObjectConverter.CInt(dt.Rows[0]["nOil92"]),
                    Oil95QTY = ObjectConverter.CInt(dt.Rows[0]["nOil95"]),
                    SolarUnitPrice = ObjectConverter.CDec(dt.Rows[0]["SolarUnitPrice"]),
                    Oil80UnitPrice = ObjectConverter.CDec(dt.Rows[0]["Oil80UnitPrice"]),
                    Oil92UnitPrice = ObjectConverter.CDec(dt.Rows[0]["Oil92UnitPrice"]),
                    Oil95UnitPrice = ObjectConverter.CDec(dt.Rows[0]["Oil95UnitPrice"]),
                    SolarTotalPrice = ObjectConverter.CDec(dt.Rows[0]["SolarTotalPrice"]),
                    Oil80TotalPrice = ObjectConverter.CDec(dt.Rows[0]["Oil80TotalPrice"]),
                    Oil92TotalPrice = ObjectConverter.CDec(dt.Rows[0]["Oil92TotalPrice"]),
                    Oil95TotalPrice = ObjectConverter.CDec(dt.Rows[0]["Oil95TotalPrice"]),
                    TotalPrice = ObjectConverter.CDec(dt.Rows[0]["nPrice"]),
                    StockTransportAmount = ObjectConverter.CDec(dt.Rows[0]["StockTransportAmount"]),
                    StockTransportUnit = ObjectConverter.CDec(dt.Rows[0]["StockTransportUnit"]),
                    Desc = ObjectConverter.Cstr(dt.Rows[0]["sDesc"]),
                };
            }
            return null;
        }

        public async Task<List<Invoice>> ListAsync()
        {
            string sql = " select * from vuw_CompanyInnvoices where nBranchId=" + CommonFolder.Common.Current.BranchId + " ";
            DataSet ds = await db.GetDataSetAsync(sql);
            DataTable dt = ds.Tables[0];
            List<Invoice> lst = new List<Invoice>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new Invoice
                    {
                        Id = ObjectConverter.CInt(row["nId"]),
                        InnvoiceNo = ObjectConverter.CInt(row["nInnvoiceNo"]),
                        dDate = ObjectConverter.CDate(row["dDate"]),
                        BranchId = ObjectConverter.CInt(row["nBranchId"]),
                        CompanyId = ObjectConverter.CInt(row["nCompany"]),
                        Company = new Company
                        {
                            Id = ObjectConverter.CInt(row["nCompany"]),
                            Name = ObjectConverter.Cstr(row["CompanyName"]),
                        },
                        StationId = ObjectConverter.CInt(row["nStationNo"]),
                        Station = new Station
                        {
                            Id = ObjectConverter.CInt(row["nStationNo"]),
                            Name = ObjectConverter.Cstr(row["StationName"]),
                        },
                        CarId = ObjectConverter.CInt(row["nCarNo"]),
                        Car = new Car
                        {
                            Id = ObjectConverter.CInt(row["nCarNo"]),
                            Name = ObjectConverter.Cstr(row["sName"]),
                        },
                        TotalPrice = ObjectConverter.CDec(row["nPrice"]),
                    });
                }
            }
            return lst;
        }

        public async Task<bool> ObjectExists(object Id)
        {
            Invoice entity = await FindByIdAsync(Id);
            if (entity != null)
                return true;
            return false;
        }

        public List<Invoice> Search(object term)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> UpdateAsync(object Id, Invoice entity)
        {
            try
            {
                ExecUpdate(Id, entity);
                Invoice OldInvoice = await FindByIdAsync(Id);
                UpdateBalance(false, OldInvoice.SolarQTY, OldInvoice.Oil80QTY, OldInvoice.Oil92QTY, OldInvoice.Oil95QTY);
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

        public bool ExecUpdate(object Id, Invoice entity)
        {
            try
            {
                db.NewFields();
                db.Table = "Innvoice";
                db.AddField("nCompany", ObjectConverter.Cstr(entity.CompanyId));
                db.AddField("nInnvoiceNo", ObjectConverter.Cstr(entity.InnvoiceNo));
                db.AddFieldstr("dDate", ObjectConverter.Cstr(entity.dDate.ToString("yyyy-MM-dd")));
                db.AddField("nStockNo", ObjectConverter.Cstr(entity.StockId));
                db.AddField("nStationNo", ObjectConverter.Cstr(entity.StationId));
                db.AddField("nCarNo", ObjectConverter.Cstr(entity.CarId));
                db.AddField("nDriverNo", ObjectConverter.Cstr(entity.DriverId));
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
                db.AddFieldstr("sDesc", ObjectConverter.Cstr(entity.Desc));
                db.AddFieldG("StockTransportUnit", ObjectConverter.Cstr(entity.StockTransportUnit));
                db.AddFieldG("StockTransportAmount", ObjectConverter.Cstr(entity.StockTransportAmount));
                db.sCondition = " nId=" + Id + " and nBranchId=" + CommonFolder.Common.Current.BranchId + " and JournalType=" + entity.JournalType + "";
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
