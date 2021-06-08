
using OilStationMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class InvoiceDepositRepository
    {
        private readonly DataBase.DB db;

        public InvoiceDepositRepository()
        {
            this.db = new DataBase.DB();

        }

        public async Task<List<InvoiceDepositViewModel>> TotalCompanyDepositVSinvoicesPerMonth()
        {
            var lst = new List<InvoiceDepositViewModel>();
            string sql = @"	select * from vuw_TotalCompanyDepositVSinvoicesPerMonth";
            DataSet ds = await db.GetDataSetAsync(sql);
            DataTable dt =ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    lst.Add(new InvoiceDepositViewModel
                    {
                        Id = Helper.ObjectConverter.CInt(item["nId"]),
                        Name = Helper.ObjectConverter.Cstr(item["sName"]),
                        TotalAmount = Helper.ObjectConverter.CDec(item["TotalDepositAmount"]),
                        TotalPrice = Helper.ObjectConverter.CDec(item["TotalInvoiceAmount"]),

                    });
                }
            }
            
            return lst;
        }

        public async Task<List<InvoiceDepositViewModel>> TotalMazotDepositVSinvoicesPerMonth()
        {
            var lst = new List<InvoiceDepositViewModel>();
            string sql = @"	select * from vuw_MazotInvoiceAmountPerMonth";
            DataSet ds = await db.GetDataSetAsync(sql);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    lst.Add(new InvoiceDepositViewModel
                    {
                        Id = Helper.ObjectConverter.CInt(item["nId"]),
                        Name = Helper.ObjectConverter.Cstr(item["sName"]),
                        TotalAmount = Helper.ObjectConverter.CDec(item["TotalDepositAmount"]),
                        TotalPrice = Helper.ObjectConverter.CDec(item["TotalInvoiceAmount"]),

                    });
                }
            }

            return lst;
        }
 
    }
}
