 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class SubledgerBalanceRepository
    {
        private readonly DataBase.DB db;

        public SubledgerBalanceRepository(DataBase.DB db)
        {
            this.db = db;
        }

        public List<SubledgerBalanceViewModel> SubledgerBalance(string SubledgerTypeId)
        {
            string sql = @"select ISNULL( sum(case when Balance<=0 then balance*-1 else 0 end),0) as Debit
            , ISNULL(sum(case when Balance >= 0 then balance else 0 end),0) as Credit from vuw_Balances
               where SubledgerTypeId = " + SubledgerTypeId + " ";

            DataTable dt = db.GetDataTable(sql);
            List<SubledgerBalanceViewModel> lst = new List<SubledgerBalanceViewModel>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    SubledgerBalanceViewModel model = new SubledgerBalanceViewModel
                    {
                        Credit = db.CDec(item["Credit"]),
                        Debit = db.CDec(item["Debit"]),
                        Balance = db.CDec(item["Credit"]) - db.CDec(item["Debit"])
                    };
                    lst.Add(model);
                }
            }
            else
            {
                SubledgerBalanceViewModel model = new SubledgerBalanceViewModel
                {
                    Credit = 0,
                    Debit = 0,
                    Balance = 0
                };
                lst.Add(model);
            }
            return lst;
        }
    }
}
