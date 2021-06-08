
using OilStationMVC.Helper;
using OilStationMVC.Models.Repositories;
using OilStationMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class FinancialRepository  
    {
        private readonly DataBase.DB db;

        public FinancialRepository()
        {

            this.db = new DataBase.DB();
        }

        public async Task<int> AddAsync(Financial entity)
        {
            try
            {
                db.NewFields();
                db.Table = "DepositCustomer";
                entity.Id = int.Parse(await db.GetNewKeyAsync("nId", "DepositCustomer"));
                db.AddField("nId", entity.Id.ToString());
                db.AddField("nBranchId", CommonFolder.Common.Current.BranchId.ToString());
                db.AddField("JournalType", ObjectConverter.Cstr(entity.JournalType));
                db.AddFieldstr("dDate", ObjectConverter.Cstr(entity.dDate.ToString("yyyy-MM-dd")));
                db.AddField("nOilCustodyType", ObjectConverter.Cstr(entity.CustodyTypeId));
                db.AddField("nCustNo", ObjectConverter.Cstr(entity.SubledgerId));
                db.AddFieldG("nAmount", ObjectConverter.Cstr(entity.Amount));
                db.AddField("nDepositNo", ObjectConverter.Cstr(entity.DepositNo));
                db.AddFieldstr("sDesc", ObjectConverter.Cstr(entity.Desc));

                db.AddField("nCarNo", ObjectConverter.Cstr(entity.SubledgerId));
                db.AddField("nCompany", ObjectConverter.Cstr(entity.SubledgerId));
                return await db.ExecInsertAsync();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<int> DeleteByIdAsync(object Id, object JournalType = null)
        {
            try
            {
                db.NewFields();
                db.Table = "DepositCustomer";
                db.sCondition = " nId=" + Id + "";
                return await db.ExecDeleteAsync();
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public async Task<FinancialViewModel> FindByIdAsync(object Id, object JournalType)
        {

            string sql = " select * from vuw_OilStationAccountant where nId=" + Id + " and JournalType=" + JournalType + " and nBranchId=" + CommonFolder.Common.Current.BranchId.ToString() + "";
            DataTable dt = await db.GetDataTableAsync(sql);
            if (dt.Rows.Count > 0)
            {
                FinancialViewModel financial = new FinancialViewModel();
                financial.Id = ObjectConverter.CInt(dt.Rows[0]["nId"]);
                financial.BranchId = ObjectConverter.CInt(dt.Rows[0]["nBranchId"]);
                financial.JournalType = ObjectConverter.CInt(dt.Rows[0]["JournalType"]);
                financial.dDate = ObjectConverter.CDate(dt.Rows[0]["dDate"]);
                financial.SubledgerId = ObjectConverter.CInt(dt.Rows[0]["SubledgerCode"]);
                financial.SubledgerTypeId = ObjectConverter.CInt(dt.Rows[0]["SubledgerType"]);
                financial.DepositNo = ObjectConverter.Cstr(dt.Rows[0]["SubledgerType"]);
                financial.Desc = ObjectConverter.Cstr(dt.Rows[0]["sDesc"]); 
                financial.Amount= ObjectConverter.CDec(dt.Rows[0]["nAmount"]); 

                return financial;

            };
            return null;
        }


        public async Task<List<FinancialViewModel>> ListAsync(string JournalType)
        {
            string sql = " select * from vuw_OilStationAccountant where Journaltype=" + JournalType + " ";
            DataTable dt = await db.GetDataTableAsync(sql);
            List<FinancialViewModel> lst = new List<FinancialViewModel>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new FinancialViewModel
                    {
                        Id = ObjectConverter.CInt(row["nId"]),
                        BranchId = ObjectConverter.CInt(row["nBranchId"]),
                        JournalType = ObjectConverter.CInt(row["JournalType"]),
                        dDate = ObjectConverter.CDate(row["dDate"]),

                        SubledgerTypeId = ObjectConverter.CInt(row["SubledgerType"]),
                        SubledgerId = ObjectConverter.CInt(row["SubledgerCode"]),
                        Subledger = new Subledger
                        {
                            Id = ObjectConverter.CInt(row["SubledgerCode"]),
                            Name = ObjectConverter.Cstr(row["SubledgerName"]),
                        },
                        Amount = ObjectConverter.CDec(row["nAmount"]),

                    });
                }
            }
            return lst;
        }

        public async Task<List<FinancialViewModel>> ListAsync(object JournalType, object SubledgerTypeId)
        {
            string sql = " select * from DepositCustomer where nId=" + SubledgerTypeId + " and JournalType=" + JournalType + "";
            DataTable dt = await db.GetDataTableAsync(sql);
            List<FinancialViewModel> lst = new List<FinancialViewModel>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new FinancialViewModel
                    {
                        Id = ObjectConverter.CInt(row["nId"]),
                        BranchId = ObjectConverter.CInt(row["BranchId"]),
                        JournalType = ObjectConverter.CInt(row["JournalType"]),
                        dDate = ObjectConverter.CDate(row["dDate"]),
                        SubledgerId = ObjectConverter.CInt(row["dDate"]),
                    });
                }
            }
            return lst;
        }

        public Task<List<Financial>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ObjectExists(object Id, object JournalType)
        {
            if (await FindByIdAsync(Id, JournalType) == null)
                return false;
            else
                return true;
        }

        public List<Financial> Search(object term)
        {
            throw new NotImplementedException();
        }
         
        public async Task<int> UpdateAsync(object Id, Financial entity)
        {
            try
            {
                db.NewFields();
                db.Table = "DepositCustomer";
                db.AddFieldstr("dDate", ObjectConverter.Cstr(entity.dDate));
                db.AddField("nOilCustodyType", ObjectConverter.Cstr(entity.CustodyTypeId));
                db.AddField("nCustNo", ObjectConverter.Cstr(entity.SubledgerId));
                db.AddFieldG("nAmount", ObjectConverter.Cstr(entity.Amount));
                db.AddField("nDepositNo", ObjectConverter.Cstr(entity.DepositNo));
                db.AddFieldstr("sDesc", ObjectConverter.Cstr(entity.Desc));

                db.AddField("nCarNo", ObjectConverter.Cstr(entity.SubledgerId));
                db.AddField("nCompany", ObjectConverter.Cstr(entity.SubledgerId));
                db.sCondition = " nId=" + Id + " and JournalType=" + entity.JournalType + " and nBranchId=" + CommonFolder.Common.Current.BranchId.ToString() + "";
                return await db.ExecUpdateAsync();
            }
            catch (Exception)
            {
                return 0;
            }
        } 
    }
}
