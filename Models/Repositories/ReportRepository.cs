
using OilStationMVC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class ReportRepository
    {
        private readonly DataBase.DB db;

        public ReportRepository(DataBase.DB db)
        {
            this.db = db;
        }

        //public List<AccountStatementViewModel> StaionCustomerAcountStatement(string sWhere=null)
        //{
        //    string sql = "select * from vuw_AccountStatement where "+sWhere+" order by dDate";
        //    DataTable dt = db.GetDataTable(sql);
        //    List<AccountStatementViewModel> lst = new List<AccountStatementViewModel>();
        //    if (dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow item in dt.Rows)
        //        {
        //            lst.Add(new AccountStatementViewModel
        //            {
        //                Id = ObjectConverter.CInt(item["Id"]),
        //                BranchId = ObjectConverter.CInt(item["BranchId"]),
        //                JournalType = ObjectConverter.CInt(item["JournalType"]),
        //                JournalName = ObjectConverter.Cstr(item["JournalName"]),
        //                dDate = ObjectConverter.Cstr(item["dDate"]),
        //                SubledgerTypeId = ObjectConverter.CInt(item["SubledgerTypeId"]),
        //                SubledgerTypeName = ObjectConverter.Cstr(item["SubledgerTypeName"]),
        //                SubledgerId = ObjectConverter.CInt(item["SubledgerId"]),
        //                SubledgerName = ObjectConverter.Cstr(item["SubledgerName"]),
        //                Credit = ObjectConverter.CDec(item["Credit"]),
        //                Debit = ObjectConverter.CDec(item["Debit"]),
        //                Amount = ObjectConverter.CDec(item["Amount"]),
        //                Balance = ObjectConverter.CDec(item["Debit"])- ObjectConverter.CDec(item["Credit"]),
        //                Telephone = ObjectConverter.Cstr(item["Telephone"]),
        //                CustomerTypeId = ObjectConverter.CInt(item["CustomerTypeId"]),
        //            });
        //        }
        //    }
        //    return lst;
        //}
    }
}
