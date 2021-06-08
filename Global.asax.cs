using OilStationMVC.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Drawing;
using System.Resources;
namespace OilStationMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //RouteTable.Routes.MapHubs(); 
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles); 

        }
        void Application_BeginRequest(object sender, EventArgs e)
        {

            var cookie = HttpContext.Current.Request.Cookies["_culture"];
            var name = cookie != null ? cookie.Value : null; 
            if (cookie != null && cookie.Value != null)
            {
                if (cookie.Value == "en")
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie.Value);
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cookie.Value);
                    CommonFolder.Common.Current.IsEnglish = true;
                }
                else
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie.Value);
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cookie.Value);
                    CommonFolder.Common.Current.IsEnglish = false;
                }
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
                CommonFolder.Common.Current.IsEnglish = true;
            }



        }
        protected void Session_Start(object sender, EventArgs e)
        { 
            var currentTime = DateTime.Now;
            HttpContext.Current.Session["LastUpdated"] = currentTime; 
        }
    }
}
