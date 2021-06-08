using OilStationMVC.AuthenticationAndAuthorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.Controllers
{
    [mAuthorize]
    public class OilProdcutPriceController : Controller
    {
        // GET: OilProdcutPrice
        public ActionResult Index()
        {
            DataBase.DB db = new DataBase.DB();
            ViewBag.NextCode = db.GetNewKey("nId", "OilProdcutPrice");
            return View();
        }
    }
}