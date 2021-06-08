using Microsoft.AspNet.Identity.Owin;
using OilStationMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.Controllers
{
    public class NotificationController : Controller
    {
        private ApplicationUserManager _userManager; 
        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public async Task<JsonResult> LoadNotifications()
        {
            var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
            Models.ApplicationUser user = await UserManager.FindByNameAsync(User.Identity.Name); 
            List<OilStationMVC.Models.Notification> lst = await NotificationRepository.ListUnReadAsync(user.Id, notificationRegisterTime);
            Session["LastUpdate"] = DateTime.Now;
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public async  Task<ActionResult> MarkAsReadAsync(int Id)
        {
            Models.ApplicationUser user = await UserManager.FindByNameAsync(User.Identity.Name); 
            await NotificationRepository.MarkAsReadAsync(Id, user.Id);
            string message = "SUCCESS";
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }


    }
}