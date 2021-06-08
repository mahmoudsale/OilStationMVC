using OilStationMVC.AuthenticationAndAuthorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.Controllers
{
    [mAuthorize]
    public class GhaznaController : Controller
    {
        // GET: Ghazna
        public ActionResult Index()
        {
            return View();
        }
    }
}