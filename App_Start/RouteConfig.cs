using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace OilStationMVC
{

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //AreaRegistration.RegisterAllAreas();

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            //    ,
            //     namespaces: new[] { "OilStationMVC.Controllers" }
            //);

            routes.MapRoute(
             name: "Default",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
             ,
              namespaces: new[] { "OilStationMVC.Controllers" }
         );

        }

    }
}
