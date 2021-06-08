using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult NotFound(string ErrorMessage)
        {
            ViewBag.ErrorMessage = ErrorMessage; 
            return View();
        }
        public ActionResult Exception()
        { 
            return View();
        }

    }
}