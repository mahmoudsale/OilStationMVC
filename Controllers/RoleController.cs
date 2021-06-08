using Microsoft.AspNet.Identity.Owin;
using OilStationMVC.AuthenticationAndAuthorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.Controllers
{
    [mAuthorize]
    public class RoleController : Controller
    {
        private ApplicationRoleManager _roleManager;
        public RoleController( ApplicationRoleManager roleManager)
        { 
            RoleManager = roleManager;
        }
        // GET: Role
        public ActionResult Index()
        {
            var roles = RoleManager.Roles;
            return View(roles); 
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
    }
}