using Newtonsoft.Json;
using OilStationMVC.AuthenticationAndAuthorization;
using OilStationMVC.Helper;
using OilStationMVC.Models;
using OilStationMVC.Models.Repositories;
using OilStationMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OilStationMVC.Controllers
{
    [mAuthorize]
    public class HomeController : Controller
    {
        ProductRepository ProductRepository = new ProductRepository();
     

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult HelpCenter()
        {
            ViewBag.Message = "Your Help Center page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Menu()
        {
            DataTable dt = LoadMenu();
            List<Models.menu> lst = MapToList(dt);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string s = js.Serialize(lst);
            List<Models.menu> listMenu = GetMenuTree(lst, 0);
            return PartialView("_Menu", listMenu);
        }


        private List<Models.menu> GetMenuTree(List<Models.menu> lst, int? ParentId)
        {
            return lst.Where(x => x.parent == ParentId).Select(x => new Models.menu()
            {
                id = x.id,
                Name = x.Name,
                parent = x.parent,
                IsForm = x.IsForm,
                Action = x.Action,
                Controller = x.Controller,
                FormParameter = x.FormParameter,
                LstChild = GetMenuTree(lst, x.id),
                MainReportName=x.MainReportName
            }).ToList();
        }
        public DataTable LoadMenu()
        {
            DataBase.DB db = new DataBase.DB();
            System.Data.DataSet ds = new System.Data.DataSet();
            //if (!Common.Class_Configration.IsUserIt)
            //{

            //    if (!string.IsNullOrEmpty(Common.Class_Configration.Licensedmodule))
            //    {
            string sql = @"select Menu.*,ReportTable.MainReportName 
                        from Menu left outer join 
                        ReportTable 
                        on menu.FormName=ReportTable.FormName 
                        where  Menu. nHide=0 and Menu. nModuleId =2
                        and Menu.FormType=2
 

                         union all 

                         select Menu. * ,'' as MainReportName from Menu
                         where  Menu. nHide=0 and Menu. nModuleId =2 
                         and Menu.FormType!=2
                         order by Menu.nModuleId,Menu.nParent , Menu.nId";
            //ds = db.GetDataSet("select Menu.*,ReportTable.MainReportName from Menu left outer join ReportTable on menu.FormName=ReportTable.FormName where  Menu. nHide=0 and Menu. nModuleId =2    order by Menu.nModuleId,Menu.nParent , Menu.nId");
            ds = db.GetDataSet(sql);


            //    }
            //    else
            //    {
            //        ds = db.GetDataSet("select * from Menu where  nHide=0 and nModuleId=2 order by nModuleId, nParent ");

            //    }
            //}
            //else
            //{

            //    ds = db.GetDataSet("select * from Menu where nHide=0 and nModuleId=2  order by nModuleId, nParent ");

            //}

            DataTable dt = ds.Tables[0];
            return dt;
        }


        public List<Models.menu> MapToList(DataTable dt)
        {
            List<Models.menu> lst = new List<Models.menu>();
            foreach (DataRow dr in dt.Rows)
            {
                Models.menu m = new Models.menu();

                m.id = ObjectConverter.CInt(dr["Nid"]);
                m.FormParameter = ObjectConverter.Cstr(dr["FormParameterMVC"]);
                m.MainReportName = ObjectConverter.Cstr(dr["MainReportName"]);
                //if (Common.Current.IsEnglish == false)
                //{
                //    //m.name = Common.MW.Cstr(dr["sMenuName"] + "     " + Common.MW.Cstr(dr["sMenuNameEng"]));
                //    m.name = Common.MW.Cstr(dr["sMenuName"]);
                //}
                //else
                //{
                //    m.name = Common.MW.Cstr(dr["sMenuName"]);
                //}
                m.Name = ObjectConverter.Cstr(dr["sMenuName"]);
                m.IsForm = ObjectConverter.CBool(dr["nform"]);
                m.parent = ObjectConverter.CInt(dr["nparent"]);
                m.nModuleId = ObjectConverter.CInt(dr["nModuleId"]);
                m.FormName = ObjectConverter.Cstr(dr["FormName"]);
                if (!string.IsNullOrEmpty(ObjectConverter.Cstr(dr["PageUrl"])))
                {
                    m.PageUrl = ObjectConverter.Cstr(dr["PageUrl"]);
                }
                else
                {
                    m.PageUrl = "#";
                }

                m.Controller = ObjectConverter.Cstr(dr["Controller"]);

                m.Action = ObjectConverter.Cstr(dr["Action"]);

                m.FormType = ObjectConverter.Cstr(dr["FormType"]);

                if (!string.IsNullOrEmpty(ObjectConverter.Cstr(dr["TableName"])))
                {
                    m.TableName = ObjectConverter.Cstr(dr["TableName"]);
                }
                lst.Add(m);

                //lst.Add(new menu { id = int.Parse(dr["Nid"].ToString()), parent = int.Parse(dr["nParent"].ToString()), name = dr["sMenuName"].ToString() });
            }
            return lst;
        }

        public ActionResult SetLanguage(string culture, string returnUrl)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            var cookie = new HttpCookie("_culture", culture);
            cookie.Expires = DateTime.Today.AddYears(1);
            Response.SetCookie(cookie);
            return Redirect(returnUrl);
        }


        #region Dashboards
        public ActionResult CommonDashboard(int SubledgerTypeId, string Title)
        {
            CommonBalanceRepository commonBalanceRepository = new CommonBalanceRepository();
            ViewBag.Title = Title;
            List<CommonBalanceViewModel> lst = commonBalanceRepository.BalanceList(SubledgerTypeId); 
            return  View(  lst);
        }
        #endregion

        #region Charts

        public ActionResult PassDataBetweenTagHelpers()
        {
            return View();
        }
        public async Task<JsonResult> CompanyInvoicesVSdepositsAsync(string Company)
        {
            BarChart barChart = new BarChart();
            Dataset invoices = new Dataset();
            invoices.label = "فواتير";
            invoices.backgroundColor = "rgb(255,0,0)";
            Dataset deposits = new Dataset();
            deposits.label = "إيداعات";

            InvoiceDepositRepository invoiceDepositRepository = new InvoiceDepositRepository();
            List<InvoiceDepositViewModel> lst = await invoiceDepositRepository.TotalCompanyDepositVSinvoicesPerMonth();
            if (lst.Any())
            {
                barChart.labels = lst.Select(x => x.Name).ToArray();
                invoices.data = lst.Select(x => x.TotalPrice).ToArray();
                deposits.data = lst.Select(x => x.TotalAmount).ToArray();
            }
            barChart.datasets[0] = invoices;
            barChart.datasets[1] = deposits;


            return Json(barChart, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> MazotInvoicesVSdepositsAsync()
        {
            BarChart barChart = new BarChart();
            Dataset invoices = new Dataset();
            invoices.label = "فواتير";
            invoices.backgroundColor = "rgb(255,0,0)";
            Dataset deposits = new Dataset();
            deposits.label = "إيداعات";
            InvoiceDepositRepository invoiceDepositRepository = new InvoiceDepositRepository();
            List<InvoiceDepositViewModel> lst = await invoiceDepositRepository.TotalMazotDepositVSinvoicesPerMonth();
            if (lst.Any())
            {
                barChart.labels = lst.Select(x => x.Name).ToArray();
                invoices.data = lst.Select(x => x.TotalPrice).ToArray();
                deposits.data = lst.Select(x => x.TotalAmount).ToArray();
            }
            barChart.datasets[0] = invoices;
            barChart.datasets[1] = deposits;

            return Json(barChart, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> LoadProductsBalanceAsync()
        {
            PieChart pieChart = new PieChart();
            pieChartDataset dataset = new pieChartDataset();

            List<Product> lst = await ProductRepository.ListAsync();
            if (lst.Any())
            {
                pieChart.labels = lst.Select(x => x.Name).ToArray();
                dataset.data = lst.Select(x => x.FirstPeriodBalance).ToArray();
            }
            pieChart.datasets[0] = dataset;
            return Json(pieChart, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> SubledgerBalance(int SubledgerTypeId)
        {
            CommonBalanceRepository commonBalanceRepository = new CommonBalanceRepository();
            SubledgerBalanceViewModel model =await commonBalanceRepository.SubledgerTotalBalance(SubledgerTypeId);
            return Json(model, JsonRequestBehavior.AllowGet);  
        }

        #endregion
 
        public ActionResult Chat() 
        { 
            ViewBag.Message = "Your chat page"; 
            return View(); 
        }
    }
}