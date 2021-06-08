using CrystalDecisions.CrystalReports.Engine;
using OilStationMVC.AuthenticationAndAuthorization;
using OilStationMVC.Helper;
using OilStationMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.Controllers
{
    [mAuthorize]
    public class ReportController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CarNawloan(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult CarNawloan(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select * from vuw_MazotDaily " + sWhere + "");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }
        public ActionResult MazotCustomerInvoice(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult MazotCustomerInvoice(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select * from vuw_MazotDaily " + sWhere + " and  journaltype=3");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }
        public ActionResult MazotAccountStatement(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult MazotAccountStatement(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select  * from vuw_OilStationAccountant " + sWhere + "  and CustomerType=1 and SubledgerType=1");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }
        public ActionResult CarAccountStatement(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult CarAccountStatement(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select  * from vuw_OilStationAccountant " + sWhere + "   and SubledgerType=3");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }
        public ActionResult Expenses(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult Expenses(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select * from vuw_DepositCustomer " + sWhere + " and JournalType=10 ");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }

        public ActionResult MazotComapnyAccountStatement(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult MazotComapnyAccountStatement(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select  * from vuw_OilStationAccountant " + sWhere + "   and SubledgerType=2");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }

        public ActionResult MazotCompanyInvoice(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult MazotCompanyInvoice(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select distinct * from vuw_MazotDailyMaster " + sWhere + " and  journaltype=3");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }
        public ActionResult MazotDeposit(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult MazotDeposit(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select * from vuw_DepositCustomer " + sWhere + " and JournalType=11");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }
        public ActionResult MazotCompanyRevesion(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult MazotCompanyRevesion(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select * from vuw_OilStationAccountant " + sWhere + " and JournalType in(3,11)");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }
        public ActionResult MazotDaily(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult MazotDaily(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            string sql = @"SELECT DISTINCT 
                         nId, nInnvoiceId, nBranchId, dDate, nCompany, Companyname, nChargeArea, ChargeAreaName, nCarNo, CarName, nDriver, DriverName, nChargUnitPrice, nTotalQuantity, nQuantityPrice, nTax, nFees, nFinalPrice, 
                         nChargeFactory, ChargeFactoryName, sDesc
                         FROM  dbo.vuw_MazotDaily";
            DataSet ds = db.GetDataSet(sql + sWhere + "and  journaltype=3 ");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }
        public ActionResult MazotCustomerBalances(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult MazotCustomerBalances(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select * from vuw_MazotDaily " + sWhere + "");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }
        public ActionResult StationInvoice(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult StationInvoice(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select  * from vuw_CompanyInnvoices " + sWhere + " ");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }
        public ActionResult StationCustomerInvoice(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult StationCustomerInvoice(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select  * from vuw_StationCustomerInnvoices " + sWhere + "");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }

        public ActionResult StationCustomrAccountStatement(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult StationCustomrAccountStatement(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select  * from vuw_OilStationAccountant " + sWhere + "  and CustomerType=2 and SubledgerType=1");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }

        public ActionResult StationDeposit(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult StationDeposit(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select * from vuw_DepositCustomer " + sWhere + " and JournalType=7");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }
        public ActionResult CustomerFinancialMoves(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult CustomerFinancialMoves(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select * from vuw_OilStationAccountant " + sWhere + " ");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }
        public ActionResult CustomerData(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult CustomerData(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select  * from vuw_AllCustomer " + sWhere + "");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }

        public ActionResult StockNawloon(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult StockNawloon(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select  * from vuw_CompanyInnvoices " + sWhere + " ");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }
        public ActionResult CountersObservation(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult CountersObservation(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select * from vuw_MazotDaily " + sWhere + "");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }

        public ActionResult StationCompanyRevision(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult StationCompanyRevision(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select * from vuw_OilStationAccountant " + sWhere + " and JournalType in(7,12)");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }
        public ActionResult CusomerTotalbalance(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult CusomerTotalbalance(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select * from vuw_CustomerAcount2 " + sWhere + "");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }
        public ActionResult MazotCustomerBalanceAmount(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult MazotCustomerBalanceAmount(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            string sql = " select Year,[Month],nCustomer,sName,nBranchId, sum(nTotalQuantity) as nTotalQuantity from vuw_MazotCustomerbalanceAmount ";
            sql += sWhere;
            sql += "  group by Year,[Month],nCustomer,sName,nBranchId";
            DataSet ds = db.GetDataSet(sql);
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }

        public ActionResult CarNawwloanCompany(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult CarNawwloanCompany(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select * from vuw_MazotDaily " + sWhere + "");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }
        public ActionResult StationBoxAcountStatement(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult StationBoxAcountStatement(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select  * from vuw_OilStationAccountant " + sWhere + "   and Journaltype in(0,1,2,6,7,8,9,10,11,13) and nGhaznaNo!=0");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }

        public ActionResult TransportAreaInvoice(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
        [HttpPost]
        public ActionResult TransportAreaInvoice(ReportViewModel model)
        {
            string sWhere = GetWhere(model);
            DataBase.DB db = new DataBase.DB();
            DataSet ds = db.GetDataSet("select  * from vuw_MazotDaily " + sWhere + " ");
            Session["ReportName"] = model.ReportName;
            Session["DataSet"] = ds;
            return Redirect("ReportForm.aspx");
        }


























        private string GetWhere(ReportViewModel model)
        {
            string sWhere = "where 1=1";
            foreach (PropertyInfo prop in model.GetType().GetProperties())
            {
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                string PropName = prop.Name;
                if (type == typeof(DateTime))
                {
                    if (PropName == "FromDate")
                    {
                        DateTime FromDate = ObjectConverter.CDate(prop.GetValue(model, null).ToString());
                        if (FromDate != default(DateTime))
                        {
                            sWhere += " and dDate>='" + FromDate.ToString("yyyy-MM-dd") + "'";
                        }
                   
                    }
                    if (PropName == "ToDate")
                    {
                        DateTime ToDate = ObjectConverter.CDate(prop.GetValue(model, null).ToString());
                        if (ToDate != default(DateTime))
                        {
                            sWhere += " and dDate<='" + ToDate.ToString("yyyy-MM-dd") + "'";
                        }
                    }

                }
                if (type == typeof(int))
                {
                    if (PropName == "SubledgerTypeId")
                    {
                        string SubledgerTypeId = prop.GetValue(model, null).ToString();
                        if (!string.IsNullOrEmpty(SubledgerTypeId) && SubledgerTypeId!="0")
                        {
                            sWhere += " and SubledgerType=" + SubledgerTypeId + "";
                        }
                    }
                    if (PropName == "FromSubledgerId")
                    {
                        string FromSubledgerId = prop.GetValue(model, null).ToString();
                        if (!string.IsNullOrEmpty(FromSubledgerId) && FromSubledgerId != "0")
                        {
                            sWhere += " and SubledgerCode>=" + FromSubledgerId + "";
                        }
                    }
                    if (PropName == "ToSubledgerId")
                    {
                        string ToSubledgerId = prop.GetValue(model, null).ToString();
                        if (!string.IsNullOrEmpty(ToSubledgerId) && ToSubledgerId != "0")
                        {
                            sWhere += " and SubledgerCode<=" + ToSubledgerId + "";
                        }
                    }

                }

            }
            return sWhere;
        }

        [HttpPost]
        public void Print()
        {
            Session["ViewName"] = "vuw_AllCustomer";
            Session["Where"] = " 1=1";
            Session["ReportName"] = "AllCustomers";
            Response.Redirect("ReportForm.aspx");
        }


    }
}