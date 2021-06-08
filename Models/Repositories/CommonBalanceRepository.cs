
using OilStationMVC.Helper;
using OilStationMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class CommonBalanceRepository
    {
        private readonly DataBase.DB db;

        public CommonBalanceRepository()
        {
            this.db = new DataBase.DB();
        }

        public   List<CommonBalanceViewModel> BalanceList(int SubledgerTypeId)
        {

            var lst = new List<CommonBalanceViewModel>();
            string sql = @"select * from vuw_OilStationSubledgerBalance where SubledgerType=" + SubledgerTypeId + "";
            DataTable dt = db.GetDataSet(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new CommonBalanceViewModel
                    {
                        SubledgerId = ObjectConverter.CInt(row["SubledgerCode"]),
                        SubledgerName = ObjectConverter.Cstr(row["SubledgerName"]),
                        Telephone= ObjectConverter.Cstr(row["Telephone"]),
                        Credit= ObjectConverter.CDec(row["Credit"]),
                        Debit = ObjectConverter.CDec(row["Debit"]),
                        Balance= ObjectConverter.CDec(row["Balance"]),
                        CustomerTypeId = ObjectConverter.CInt(row["CustomerTypeId"]),
                        SubledgerTypeId = ObjectConverter.CInt(row["SubledgerType"]),

                    });
                }

            }
            return lst;
        }
         
        public async Task<SubledgerBalanceViewModel> SubledgerTotalBalance(int SubledgerTypeId)
        {
            string sql = @"select ISNULL( sum(case when Balance<=0 then balance*-1 else 0 end),0) as Debit
            , ISNULL(sum(case when Balance >= 0 then balance else 0 end),0) as Credit from vuw_CustomerAcount2
               where SubledgerType = " + SubledgerTypeId + " and CustomerType !=4 and nBranchId="+CommonFolder.Common.Current.BranchId+"";

            DataTable dt =await db.GetDataTableAsync(sql);
            SubledgerBalanceViewModel model = new  SubledgerBalanceViewModel ();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    model.Credit = db.CDec(item["Credit"]);
                    model.Debit = db.CDec(item["Debit"]);
                    model.Balance = db.CDec(item["Credit"]) - db.CDec(item["Debit"]);
          
                }
            }
            else
            {
                model.Credit = 0;
                model.Debit = 0;
                model.Balance =0;
            }
            return model;
        }
    }
}
