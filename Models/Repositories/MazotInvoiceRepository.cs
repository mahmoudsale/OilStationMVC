
using OilStationMVC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static OilStationMVC.CommonFolder.Common;

namespace OilStationMVC.Models.Repositories
{
    public class MazotInvoiceRepository
    {
        private readonly DataBase.DB db;

        public MazotInvoiceRepository()
        {
            this.db = new DataBase.DB();
        }

        public async Task<bool> AddAsync(MazotInvoice entity)
        {
            try
            {
                if(ExecInsert(entity))
                {
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

        private bool ExecInsert(MazotInvoice entity)
        {
            try
            {
                string sWhereForId = " nBranchId=" + Current.BranchId + " and JournalType=" + entity.JournalType + "";
                entity.Id = int.Parse(db.GetNewKey("nId", "MazotDaily", sWhereForId));
                if (entity.lstDetails.Count > 0)
                {
                    foreach (var item in entity.lstDetails)
                    {
                        db.NewFields();
                        db.Table = "MazotDaily";
                        string sWhereForGridSerial = " nBranchId=" + Current.BranchId + " and JournalType=" + entity.JournalType + " and nId=" + entity.Id + "";
                        int GridSerial = ObjectConverter.CInt(db.GetNewKey("nGridSerial", "MazotDaily", sWhereForGridSerial));
                        db.AddField("nId", entity.Id.ToString());
                        db.AddField("nBranchId", Current.BranchId.ToString());
                        db.AddField("nGridSerial", GridSerial.ToString());
                        db.AddField("JournalType", ObjectConverter.Cstr(entity.JournalType));
                        db.AddField("nInnvoiceId", ObjectConverter.Cstr(entity.InnvoiceNo));
                        db.AddFieldstr("dDate", ObjectConverter.Cstr(entity.dDate.ToString("yyyy-MM-dd")));
                        db.AddField("nCompany", ObjectConverter.Cstr(entity.CompanyId));
                        db.AddField("nChargeFactory", ObjectConverter.Cstr(entity.ChargeCustomerId));
                        db.AddField("nChargeArea", ObjectConverter.Cstr(entity.ChargeAreaId));
                        db.AddField("nCarNo", ObjectConverter.Cstr(entity.CarId));
                        db.AddField("nDriver", ObjectConverter.Cstr(entity.DriverId));
                        db.AddFieldG("nChargUnitPrice", ObjectConverter.Cstr(entity.UnitPrice));
                        db.AddFieldG("nTotalQuantity", ObjectConverter.Cstr(entity.Qty));
                        db.AddFieldG("nQuantityPrice", ObjectConverter.Cstr(entity.QtyPrice));
                        db.AddFieldG("nTax", ObjectConverter.Cstr(entity.Tax));
                        db.AddFieldG("nFees", ObjectConverter.Cstr(entity.Fee));
                        db.AddFieldG("nFinalPrice", ObjectConverter.Cstr(entity.TotalPrice));
                        db.AddFieldstr("sDesc", ObjectConverter.Cstr(entity.Desc));
                        db.AddField("nCustomer", ObjectConverter.Cstr(item.CustomerId));
                        db.AddFieldG("nCustomerUnitPrice", ObjectConverter.Cstr(item.UnitPrice));
                        db.AddFieldG("nQuantity", ObjectConverter.Cstr(item.Qty));
                        db.AddFieldG("nPrice", ObjectConverter.Cstr(item.QtyPrice));
                        db.AddFieldG("nFeePerCustomer", ObjectConverter.Cstr(item.Fee));
                        db.AddField("nDisChargeArea", ObjectConverter.Cstr(item.AreaTransportId));
                        db.AddFieldG("nDischargeAreaUnitCost", ObjectConverter.Cstr(item.AreaTransportUnit));
                        db.AddFieldG("nDischargeAreaCost", ObjectConverter.Cstr(item.AreaTransportAmount));
                        db.ExecInsertInTrans();
                    } 
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
            return true;
        }

        public async Task<bool> ObjectExists(object Id, string JournalType)
        {
            return ObjectConverter.CBool(await FindByIdAsync(Id, JournalType));
        }
         
        public async Task<bool> DeleteByIdAsync(object Id, string JournalType)
        {
            try
            {
                ExecDelete(Id,JournalType);
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
         
        public bool ExecDelete(object Id, string JournalType)
        {
            try
            {
                db.NewFields();
                db.Table = "MazotDaily";
                db.sCondition = " nId=" + Id + " and nBranchId=" + Current.BranchId + " and JournalType=" + JournalType + "";
                db.ExecDeleteInTrans();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<MazotInvoice> FindByIdAsync(object Id, string JournalType)
        {
            MazotInvoice mazotInvoice = null;
            try
            {
                string sql = "select * from MazotDaily where  JournalType="+JournalType+" and nBranchId="+Current.BranchId+" and nId="+Id+" ";
                DataTable dt = await db.GetDataTableAsync(sql);
                if(dt.Rows.Count>0)
                {
                    mazotInvoice = new MazotInvoice
                    {
                        Id = ObjectConverter.CInt(dt.Rows[0]["nId"]),
                        BranchId = ObjectConverter.CInt(dt.Rows[0]["nBranchId"]),
                        JournalType = ObjectConverter.CInt(dt.Rows[0]["JournalType"]),
                        InnvoiceNo = ObjectConverter.CInt(dt.Rows[0]["nInnvoiceId"]),
                        dDate = ObjectConverter.CDate(dt.Rows[0]["dDate"]),
                        CompanyId = ObjectConverter.CInt(dt.Rows[0]["nCompany"]),
                        ChargeCustomerId = ObjectConverter.CInt(dt.Rows[0]["nChargeFactory"]),
                        ChargeAreaId = ObjectConverter.CInt(dt.Rows[0]["nChargeArea"]),
                        CarId = ObjectConverter.CInt(dt.Rows[0]["nCarNo"]),
                        DriverId = ObjectConverter.CInt(dt.Rows[0]["nDriver"]),
                        UnitPrice = ObjectConverter.CDec(dt.Rows[0]["nChargUnitPrice"]),
                        Qty = ObjectConverter.CDec(dt.Rows[0]["nTotalQuantity"]),
                        QtyPrice = ObjectConverter.CDec(dt.Rows[0]["nQuantityPrice"]),
                        Tax = ObjectConverter.CDec(dt.Rows[0]["nTax"]),
                        Fee = ObjectConverter.CDec(dt.Rows[0]["nFees"]),
                        TotalPrice = ObjectConverter.CDec(dt.Rows[0]["nFinalPrice"]),
                        Desc = ObjectConverter.Cstr(dt.Rows[0]["sDesc"]),
                        lstDetails = GetInvoiceListeDetails(dt)

                    };
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return mazotInvoice;
        }

        private List<MazotInvoiceDetails> GetInvoiceListeDetails(DataTable dt)
        {
            List<MazotInvoiceDetails> lst = new List<MazotInvoiceDetails>();
            foreach (DataRow item in dt.Rows)
            {
                lst.Add(new MazotInvoiceDetails
                {
                    CustomerId= ObjectConverter.CInt(dt.Rows[0]["nCustomer"]),
                    UnitPrice= ObjectConverter.CDec(dt.Rows[0]["nCustomerUnitPrice"]),
                    Qty= ObjectConverter.CDec(dt.Rows[0]["nQuantity"]),
                    QtyPrice= ObjectConverter.CDec(dt.Rows[0]["nPrice"]),
                    AreaTransportId= ObjectConverter.CInt(dt.Rows[0]["nDisChargeArea"]),
                    AreaTransportUnit= ObjectConverter.CDec(dt.Rows[0]["nDischargeAreaUnitCost"]),
                    AreaTransportAmount= ObjectConverter.CDec(dt.Rows[0]["nDischargeAreaCost"]),
                    Fee= ObjectConverter.CDec(dt.Rows[0]["nFeePerCustomer"]),
                    TotalPrice= ObjectConverter.CDec(dt.Rows[0]["nPrice"]),

                });
            }
            return lst;
        }

        public async Task<List<MazotInvoice>> ListAsync(string JournalType)
        {
            //string sql = " select * from vuw_MazotDaily where JournalType=" + JournalType + " and nBranchId=" + Current.BranchId + " ";
            string sql = "select distinct nId,nInnvoiceId,nBranchId,dDate,nCompany,Companyname,nChargeArea,ChargeAreaName,nCarNo,CarName,nDriver,DriverName";
            sql += ", nChargUnitPrice,nTotalQuantity,nQuantityPrice,nTax,nFees,nFinalPrice,nChargeFactory,ChargeFactoryName,  ChargeFactoryType,sDesc";
            sql += ", JournalType,JournalName";
            sql += " from vuw_MazotDaily ";
            sql += " where JournalType=" + JournalType + " and nBranchId=" + Current.BranchId + " ";

            DataSet ds = await db.GetDataSetAsync(sql);
            DataTable dt = ds.Tables[0];
            List<MazotInvoice> lst = new List<MazotInvoice>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new MazotInvoice
                    {
                        Id = ObjectConverter.CInt(row["nId"]),
                        InnvoiceNo = ObjectConverter.CInt(row["nInnvoiceId"]),
                        dDate = ObjectConverter.CDate(row["dDate"]),
                        BranchId = ObjectConverter.CInt(row["nBranchId"]),
                        CompanyId = ObjectConverter.CInt(row["nCompany"]),
                        Company = new Company
                        {
                            Id = ObjectConverter.CInt(row["nCompany"]),
                            Name = ObjectConverter.Cstr(row["Companyname"]),
                        },

                        CarId = ObjectConverter.CInt(row["nCarNo"]),
                        Car = new Car
                        {
                            Id = ObjectConverter.CInt(row["nCarNo"]),
                            Name = ObjectConverter.Cstr(row["CarName"]),
                        },
                        ChargeCustomerId = ObjectConverter.CInt(row["nChargeFactory"]),
                        ChargeCustomer = new Customer
                        {
                            Id = ObjectConverter.CInt(row["nChargeFactory"]),
                            Name = ObjectConverter.Cstr(row["ChargeFactoryName"]),
                        },
                        ChargeAreaId = ObjectConverter.CInt(row["nChargeArea"]),
                        ChargeArea = new ChargeArea
                        {
                            Id = ObjectConverter.CInt(row["nChargeArea"]),
                            Name = ObjectConverter.Cstr(row["ChargeAreaName"]),
                        },
                        DriverId = ObjectConverter.CInt(row["nDriver"]),
                        Driver = new Driver
                        {
                            Id = ObjectConverter.CInt(row["nDriver"]),
                            Name = ObjectConverter.Cstr(row["DriverName"]),
                        },
                        Qty = ObjectConverter.CDec(row["nTotalQuantity"]),
                        QtyPrice = ObjectConverter.CDec(row["nQuantityPrice"]),
                        Fee = ObjectConverter.CDec(row["nFees"]),
                        Tax = ObjectConverter.CDec(row["nTax"]),
                        UnitPrice = ObjectConverter.CDec(row["nTax"]),
                        TotalPrice = ObjectConverter.CDec(row["nFinalPrice"]),
                        Desc = ObjectConverter.Cstr(row["sDesc"]),
                        //---------------------------------------------------

                    });
                }
            }
            return lst;
        }
 
        public List<Invoice> Search(object term)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(object Id, MazotInvoice entity)
        {
            try
            {
                if(ExecDelete(Id,entity.JournalType.ToString()))
                {
                    if(ExecInsert(entity))
                    {
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
                    else
                    {
                        return false;
                    }
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

    }
}
